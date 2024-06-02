using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Models;

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
}