using LibraryManagement.Domain.Models;

namespace LibraryManagement.Application.Interfaces.Repositories;

public interface IBookBorrowingRequestDetailsRepository : IBaseRepository<BookBorrowingRequestDetails>
{
    Task<bool> IsBookCurrentlyRequested(Guid bookId, Guid userId);
    Task<bool> IsRequestExpired(Guid requestId);
}