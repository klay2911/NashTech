using LibraryManagement.Application.Base;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Application.Interfaces.Repositories;

public interface IBookBorrowingRequestRepository : IBaseRepository<BookBorrowingRequest>
{
    Task<List<BookBorrowingRequest>> GetRequestsByUserAndMonthAsync(Guid readerId, int month);
}