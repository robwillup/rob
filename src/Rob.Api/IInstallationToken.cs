using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Rob.Api;

public interface IInstallationToken
{
    public Task<InstallationToken> GetInstallationTokenAsync(JwtSecurityToken jwt);
}
