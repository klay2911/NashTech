using LibraryManagement.Application.DTOs.AuthDTOs;
using LibraryManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebAPI.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost("Login")]
    public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest dto)
    {
        var result = await _userService.LoginAsync(dto);
        return Ok(result);
    }
    [HttpPost("register")]
    public async Task<ActionResult<LoginResponse>> RegisterAsync(RegisterRequest dto)
    {
        var result = await _userService.RegisterAsync(dto);
        return Ok(result);
    }
}