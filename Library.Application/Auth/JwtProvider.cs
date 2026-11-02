using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Library.Application.Interfaces;
using Library.Domain.Permissions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Library.Application.Auth;

public class JwtProvider : IJwtProvider
{
    private readonly IConfiguration  _configuration;
    
    
    public JwtProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(User user)
    {
        var secretKey = _configuration.GetSection("JwtSettings").GetValue<string>("Secret");
        var expire =  _configuration.GetSection("JwtSettings").GetValue<TimeSpan>("Expire");
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.Name),
        };

        var signingCredential = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredential,
            expires: DateTime.UtcNow.AddDays(7));
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}