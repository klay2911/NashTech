using LibraryManagement.Application.DTOs.AuthDTOs;
using LibraryManagement.Application.Interfaces.Helpers;
using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IAuthHelper _authHelpers;

    public UserService(IUserRepository userRepository, ITokenService tokenService, IAuthHelper authHelper)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _authHelpers = authHelper;
    }
    
    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }
    
    public async Task<LoginResponse> LoginAsync(LoginRequest dto)
    {
        var getUser = await _userRepository.FindUserByEmailAsync(dto.Email);
        if (getUser == null)
            return new LoginResponse(false, "User not found");

        var checkPassword = _authHelpers.VerifyPassword(dto.Password, getUser.Password);
        if (checkPassword)
        {
            return new LoginResponse(true, "Login success", _tokenService.GenerateJWT(getUser));
        }
        return new LoginResponse(false, "Invalid credentials");
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest dto)
    {
        var getUser = await _userRepository.FindUserByEmailAsync(dto.Email);
        if (getUser != null)
            return new RegisterResponse(false, "User already exist");

        _userRepository.AddAsync(new User
        {
            FirstName = dto.FirstName,
            Email = dto.Email,
            Password = _authHelpers.HashPassword(dto.Password),
            Role = Domain.Enum.Role.Reader,
            CreatedAt = DateTime.UtcNow,
            ModifyAt = DateTime.UtcNow,
            CreatedBy = "VuLa",
            ModifyBy = "VuLa",
        });
        await _userRepository.SaveAsync();
        return new RegisterResponse(true, "Register success");
    }
}