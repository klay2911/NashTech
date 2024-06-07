using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Enum;
using LibraryManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class BookBorrowingRequestDetailsRepository : BaseRepository<BookBorrowingRequestDetails>, IBookBorrowingRequestDetailsRepository
{
    public BookBorrowingRequestDetailsRepository(LibraryContext context) : base(context)
    {
    }

    public async Task<BookBorrowingRequestDetails> IsBookCurrentlyRequested(Guid bookId, Guid userId)
    {
        return await Context.BookBorrowingRequestDetails
            .Include(r => r.BookBorrowingRequest)
            .Where(r => r.BookId == bookId && r.BookBorrowingRequest.RequestorId == userId && (r.BookBorrowingRequest.Status == RequestStatus.Waiting || (r.BookBorrowingRequest.Status == RequestStatus.Approved && r.BookBorrowingRequest.ExpiryDate < DateTime.Now)))
            .FirstOrDefaultAsync();
    }
}