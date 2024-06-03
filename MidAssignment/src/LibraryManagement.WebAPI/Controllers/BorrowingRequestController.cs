using System.Security.Claims;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BorrowingRequestController : ControllerBase
{
    private readonly IBookBorrowingRequestService _borrowingRequestService;
    private Guid UserId => Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
    private string Email => Convert.ToString(User.Claims.First(c => c.Type == ClaimTypes.Name).Value);
    public BorrowingRequestController(IBookBorrowingRequestService borrowingRequestService)
    {
        _borrowingRequestService = borrowingRequestService;
    }
    
    [Authorize(Roles = nameof(Role.Reader))]
    [HttpPost]
    public async Task<IActionResult> RequestBorrowAsync(List<Guid> bookIds)
    {
        // var UserId = new Guid();
        // var Email = "vu@gmail.com";
        var result = await _borrowingRequestService.RequestBorrowAsync(UserId, Email, bookIds);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetWaitingRequests(int pageNumber, int pageSize, string searchTerm = "")
    {
        var result = await _borrowingRequestService.GetWaitingRequests(pageNumber, pageSize, searchTerm);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRequestByIdAsync(Guid id)
    {
        var result = await _borrowingRequestService.GetRequestByIdAsync(id);
        return Ok(result);
    }
    
    [HttpPut("status")]
    [Authorize(Roles = nameof(Role.Librarian))]
    public async Task<IActionResult> UpdateRequestStatus(Guid requestId, RequestStatus status)
    {
        var UserId = new Guid();
        var Email = "vu@gmail.com";
        var res = await _borrowingRequestService.ManageBorrowingRequest(UserId, Email, requestId, status);
        return Ok(res);
    }
}