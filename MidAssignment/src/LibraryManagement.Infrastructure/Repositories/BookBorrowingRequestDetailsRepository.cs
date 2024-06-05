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
    // public async Task<BookBorrowingRequestDetails> GetBookBorrowingRequestDetails(Guid requestId)
    // {
    //     return await Context.BookBorrowingRequestDetails
    //         .Include(r => r.Book)
    //         .Where(r => r. == requestId)
    //         .ToListAsync();
    // }
    public async Task<bool> IsBookCurrentlyRequested(Guid bookId, Guid userId)
    {
        var requestDetail = await Context.BookBorrowingRequestDetails
            .Include(r => r.BookBorrowingRequest)
            .Where(r => r.BookId == bookId && r.BookBorrowingRequest.ApproverId == userId && (r.BookBorrowingRequest.Status == RequestStatus.Waiting || r.BookBorrowingRequest.Status == RequestStatus.Approved))
            .FirstOrDefaultAsync();

        return requestDetail != null;
    }

    public async Task<bool> IsRequestExpired(Guid requestId)
    {
        var request = await Context.BookBorrowingRequests.FindAsync(requestId);

        if (request == null)
        {
            throw new Exception("Request not found");
        }

        return request.ExpiryDate < DateTime.UtcNow;
    }
}