namespace LibraryManagement.Application.Interfaces.Helpers;

public interface IAuthHelper
{
    bool VerifyPassword(string providedPassword, string storedPassword);

    string HashPassword(string password);
}