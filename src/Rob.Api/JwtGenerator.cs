using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Security.Cryptography;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Rob.Api;

public class JwtGenerator : IJwtGenerator
{
    private readonly RSA _rsa;
    private readonly IConfiguration _config;

    public JwtGenerator(IConfiguration config)
    {
        _config = config;
        _rsa = new RSACryptoServiceProvider();
        string keyContent = File.ReadAllText(_config["GitHub:JwtSigningKey"]);
        ReadOnlySpan<char> pem = new(keyContent.ToCharArray());
        _rsa.ImportFromPem(pem);
    }

    public JwtSecurityToken GenerateToken()
    {
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
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new JwtSecurityToken(tokenHandler.WriteToken(token));
    }
}
