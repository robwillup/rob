using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace Rob.Api;

public class JwtGenerator
{
    public string GenerateToken()
    {
        // TODO: Use PEM to sign the JWT.
        throw new NotImplementedException();
        //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //var claims = new[]
        //{
        //        new Claim(ClaimTypes.NameIdentifier,user.Username),
        //        new Claim(ClaimTypes.Role,user.Role)
        //    };
        //var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //    _config["Jwt:Audience"],
        //    claims,
        //    expires: DateTime.Now.AddMinutes(15),
        //    signingCredentials: credentials);


        //return new JwtSecurityTokenHandler().WriteToken(token);

    }
}
