using System.Security.Claims;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateJwt (IEnumerable<Claim>? additionalClaims = null);
    string GenerateJWT (User user, IEnumerable<Claim>? additionalClaims = null);
}