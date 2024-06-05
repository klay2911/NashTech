using LibraryManagement.Application.Common.Models;
using LibraryManagement.Application.DTOs.BookDTOs;
using LibraryManagement.Application.DTOs.BorrowingRequestDTOs;
using LibraryManagement.Domain.Enum;

namespace LibraryManagement.Application.Interfaces.Services;

public interface IBookBorrowingRequestService
{
    Task<PaginatedList<BorrowingRequestResponse>> GetWaitingRequests(int pageNumber, int pageSize, string searchTerm = "");

    Task<BorrowingRequestResponse> GetRequestByIdAsync(Guid requestId);
    
    Task<string> RequestBorrowAsync (Guid readerId, string email, List<Guid> bookIds);

    Task<PaginatedList<BookResponse>> GetUserBorrowedBooks(Guid userId, int pageNumber, int pageSize, string searchTerm = "");
    Task<bool> ManageBorrowingRequest(Guid librarianId, string email, Guid requestId, RequestStatus status);
    
    
    //<> GetRequestByIdAsync(Guid requestId);

    // Task RequestBorrowingBook(int userId, int bookId);
    //
    // Task<bool> CheckUserRequestLimit(int userId);
    //
    // Task ManageBorrowingRequest(int requestId, string status, int acceptorId);
}