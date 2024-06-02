using LibraryManagement.Application.Base;
using LibraryManagement.Application.Common.Models;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Application.Interfaces.Repositories;

public interface IBookRepository : IBaseRepository<Book>
{
    IQueryable<Book> GetBooksQuery();
}