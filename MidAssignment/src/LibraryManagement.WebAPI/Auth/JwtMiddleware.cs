using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.Domain.Configs;
using Microsoft.Extensions.Options;

namespace LibraryManagement.WebAPI.Auth;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;
    public JwtMiddleware (RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
    }
    public async Task InvokeAsync(HttpContext context, IUserService userService)
    {
        if (context is null)
        {
            throw new ArgumentException(nameof(context));
        }
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
        {
            //var userId = JwtUtils.ValidateToken(token);
            // if (userId != null)
            // {
            //     context.Items ["User"] = userService.GetByIdAsync(userId.Value);
            // }
        }
        await _next(context);
    }
}