using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Bookstore.Services;

public class AuthenticationService
{
    public string CreateToken(string username, string password)
    { 
        string role = username.ToLower() switch
        {
            "admin" => "ADMIN",
            "seller" => "SELLER",
            _ => "BUYER"
        };

        var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };
         
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HakanTarik_Cok_Gizli_Ve_Cok_Uzun_Anahtar_2026_!_"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenOptions = new JwtSecurityToken(
            issuer: "BookstoreAPI",
            audience: "BookstoreFrontend",
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
}