using Microsoft.AspNetCore.Mvc;
using LoginRequest = Middleware.Models.LoginRequest;

namespace Middleware.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LogController : ControllerBase
{
    [HttpPost]
    public Task<IActionResult> Log([FromBody] LoginRequest loginRequest)
    {
        var logMessage = $"Id: {loginRequest.Id}, Message: {loginRequest.Message}";
        return Task.FromResult<IActionResult>(Ok(logMessage));
    }
}