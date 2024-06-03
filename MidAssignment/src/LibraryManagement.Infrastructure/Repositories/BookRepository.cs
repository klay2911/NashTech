using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    public BookRepository(LibraryContext context) : base(context)
    {
    }
    public IQueryable<Book> GetBooksQuery()
    {
        return Context.Books;
    }

    public override async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await Context.Set<Book>().Include(x => x.Category).ToListAsync();
    }
}