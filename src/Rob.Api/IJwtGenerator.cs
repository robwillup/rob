using System.IdentityModel.Tokens.Jwt;

namespace Rob.Api;

public interface IJwtGenerator
{
    public JwtSecurityToken GenerateToken();
}
