using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class Token{

    public static string TokenCreation(int id, string name, string lastName, string email, string ensure_num, string role){
        var key = new ConfigurationBuilder().
        SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json").Build()
        .GetSection("secretKey").Value;

        var issuer = new ConfigurationBuilder().
        SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json").Build()
        .GetSection("issuer").Value;

        var claims = new List<Claim>(){
            new Claim("id", id.ToString()),
            new Claim("name", $"{name} {lastName}"),
            new Claim("email", email),
            new Claim("ensure_num", ensure_num),
            new Claim("role", role)
        };

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
        var signing = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            claims: claims,
            signingCredentials: signing,
            expires: DateTime.Now.AddDays(1)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}