using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LibraryManagement.Application.Interfaces.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagement.Infrastructure.Helper;

public class AuthHelpers : IAuthHelper
{
    private readonly IConfiguration _configuration;
    private readonly LibraryContext _context;

    public AuthHelpers(IConfiguration configuration, LibraryContext context)
    {
        _configuration = configuration;
        _context = context;
    }
    public async Task<string> GenerateJwtToken(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null || !VerifyPassword(password, user.Password))
        {
            return null;
        }

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool VerifyPassword(string providedPassword, string storedPassword)
    {
        var hashedProvidedPassword = HashPassword(providedPassword);

        return hashedProvidedPassword == storedPassword;
    }
    public string HashPassword(string password)
    { 
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }
}
