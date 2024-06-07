using LibraryManagement.Application.DTOs.AuthDTOs;

namespace LibraryManagement.Application.Interfaces.Services;

public interface IUserService
{
    Task<LoginResponse> LoginAsync(LoginRequest dto);

    Task<RegisterResponse> RegisterAsync(RegisterRequest dto);
    
    
}