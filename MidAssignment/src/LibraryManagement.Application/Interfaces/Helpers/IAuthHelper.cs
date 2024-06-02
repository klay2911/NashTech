namespace LibraryManagement.Application.Interfaces.Helpers;

public interface IAuthHelper
{
    Task<string> GenerateJwtToken(string email, string password);

    bool VerifyPassword(string providedPassword, string storedPassword);

    string HashPassword(string password);
}