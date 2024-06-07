using LibraryManagement.Domain.Models;

namespace LibraryManagement.Application.Interfaces.Repositories;

public interface IBookBorrowingRequestDetailsRepository : IBaseRepository<BookBorrowingRequestDetails>
{
    Task<BookBorrowingRequestDetails> IsBookCurrentlyRequested(Guid bookId, Guid userId);
}