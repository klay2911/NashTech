using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagement.Infrastructure.Services;

public sealed class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration) =>
        new()
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            IssuerSigningKey = GetSecurityKey(configuration)
        };

        public string GenerateJwt(IEnumerable<Claim>? additionalClaims = null)
        {
            var securityKey = GetSecurityKey(_configuration);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expireInMinutes = Convert.ToInt32(_configuration["Jwt:ExpireMinutes"] ?? "10000");

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            if (additionalClaims?.Any() == true)
                claims.AddRange(additionalClaims!);

            var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"],
                audience: "*",
              claims: claims,
              expires: DateTime.Now.AddMinutes(expireInMinutes),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateJWT(User user, IEnumerable<Claim>? additionalClaims = null)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, (user.Email ?? string.Empty)),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                };
            if (additionalClaims?.Any() == true)
                claims.AddRange(additionalClaims!);

            return GenerateJwt(claims);
        }

        private static SymmetricSecurityKey GetSecurityKey(IConfiguration _configuration) =>
            new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
    }