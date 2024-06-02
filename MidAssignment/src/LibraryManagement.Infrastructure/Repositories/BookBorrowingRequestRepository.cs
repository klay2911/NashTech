using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class BookBorrowingRequestRepository : BaseRepository<BookBorrowingRequest>, IBookBorrowingRequestRepository
{
    public BookBorrowingRequestRepository(LibraryContext context) : base(context)
    {
    }
    public async Task<List<BookBorrowingRequest>> GetRequestsByUserAndMonthAsync(Guid userId, int month)
    {
        return await Context.BookBorrowingRequests
            .Where(r => r.DateRequested != null && r.RequestorId == userId && r.DateRequested.Value.Month == month)
            .ToListAsync();
    }
}