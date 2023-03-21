using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Keys.Cryptography;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Rob.Api;

public interface IJwtGenerator
{
    public JwtSecurityToken GenerateToken();
}
