using System.Security.Claims;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Application.Interfaces.Helpers;

public interface ITokenService
{
    //string GenerateJWT (IEnumerable<Claim>? additionalClaims = null);
    string GenerateJWT (User user, IEnumerable<Claim>? additionalClaims = null);
}