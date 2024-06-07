using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class BookBorrowingRequestRepository : BaseRepository<BookBorrowingRequest>, IBookBorrowingRequestRepository
{
    public BookBorrowingRequestRepository(LibraryContext context) : base(context)
    {
    }
    public override async Task<BookBorrowingRequest> GetByIdAsync(Guid requestId)
    {
        return (await Context.BookBorrowingRequests
            .Include(r => r.BookBorrowingRequestDetails)
            .Where( book => !book.IsDeleted)
            .FirstOrDefaultAsync(r => r.RequestId == requestId))!;
    }
    public async Task<List<BookBorrowingRequest>> GetRequestsByUserAndMonthAsync(Guid readerId, int month)
    {
        return await Context.BookBorrowingRequests
            .Where(r => r.DateRequested != null && r.RequestorId == readerId && r.DateRequested.Value.Month == month)
            .ToListAsync();
    }

    public async Task<IEnumerable<BookBorrowingRequest>> GetAllWithUserAsync()
    {
        var requests = await Context.Set<BookBorrowingRequest>()
            .Include(r => r.User)
            .Include(r => r.Approver)
            .Include(r => r.BookBorrowingRequestDetails)
            .Where( book => !book.IsDeleted)
            .ToListAsync();
        
        return requests;
    }

    
    public async Task<IEnumerable<BookBorrowingRequest>> GetAllRequestsByUserAsync(Guid userId)
    {
        return await Context.BookBorrowingRequests
            .Include(r => r.BookBorrowingRequestDetails)
            .ThenInclude(d => d.Book)
            .Where(r => r.RequestorId == userId)
            .ToListAsync();
    }
}