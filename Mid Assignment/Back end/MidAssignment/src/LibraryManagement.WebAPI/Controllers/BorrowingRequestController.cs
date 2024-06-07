using System.Security.Claims;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebAPI.Controllers;

[Route("api/requests")]
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
    
    [HttpPost]
    [Authorize(Roles = nameof(Role.Reader))]
    public async Task<IActionResult> CreateBorrowRequestAsync(List<Guid> bookIds)
    {
        var result = await _borrowingRequestService.RequestBorrowAsync(UserId, Email, bookIds);
        return Ok(result);
    }

    [HttpGet]
    [Authorize(Roles = nameof(Role.Librarian))]
    public async Task<IActionResult> GetBorrowRequestsAsync(int pageNumber, int pageSize, string searchTerm = "")
    {
        var result = await _borrowingRequestService.GetRequests(pageNumber, pageSize, searchTerm);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = nameof(Role.Librarian))]
    public async Task<IActionResult> GetBorrowRequestByIdAsync(Guid id)
    {
        var result = await _borrowingRequestService.GetRequestByIdAsync(id);
        return Ok(result);
    }
    
    [HttpGet("user/{userId}")]
    [Authorize(Roles = nameof(Role.Reader))]
    public async Task<IActionResult> GetUserBorrowedBooksAsync(Guid userId, int pageNumber, int pageSize, string searchTerm = "")
    {
        var result = await _borrowingRequestService.GetUserBorrowedBooks(userId, pageNumber, pageSize, searchTerm);
        return Ok(result);
    }
    
    [HttpPut("{requestId}")]
    [Authorize(Roles = nameof(Role.Librarian))]
    public async Task<IActionResult> UpdateBorrowRequestStatusAsync(Guid requestId, RequestStatus status)
    {
        var res = await _borrowingRequestService.ManageBorrowingRequest(UserId, Email, requestId, status);
        return Ok(res);
    }
}