using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Security.Cryptography;
using System.IO;
using Microsoft.Extensions.Configuration;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Keys.Cryptography;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rob.Api;

public class JwtGenerator : IJwtGenerator
{
    private RSA _rsa;
    private readonly IConfiguration _config;

    public JwtGenerator(IConfiguration config)
    {
        _config = config;
    }

    public JwtSecurityToken GenerateToken()
    {
        Uri kvUri = new("https://rob.vault.azure.net/");

        var client = new SecretClient(kvUri, new DefaultAzureCredential());

        var secret = client.GetSecret("rob-api-github");

        _rsa = new RSACryptoServiceProvider();
        ReadOnlySpan<char> pem = new(secret.Value.Value.ToCharArray());
        _rsa.ImportFromPem(pem);

        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("iat", DateTime.UtcNow.AddSeconds(-60).ToString()),
                new Claim("exp", DateTime.UtcNow.AddMinutes(7).ToString()),
                new Claim("iss", "297682")
            }),
            Expires = DateTime.UtcNow.AddMinutes(7),
            SigningCredentials = new SigningCredentials(new RsaSecurityKey(_rsa), SecurityAlgorithms.RsaSha256Signature)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return new JwtSecurityToken(tokenHandler.WriteToken(token));
    }
}
