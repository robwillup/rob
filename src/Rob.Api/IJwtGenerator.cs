using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Keys.Cryptography;
using System.IdentityModel.Tokens.Jwt;

namespace Rob.Api;

public interface IJwtGenerator
{
    public JwtSecurityToken GenerateToken(CryptographyClient cryptoClient, KeyVaultKey key);
}
