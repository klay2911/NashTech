using LibraryManagement.Application.Common.Models;
using LibraryManagement.Application.DTOs.BookDTOs;

namespace LibraryManagement.Application.Interfaces.Services;

public interface IBookService
{
    Task<PaginatedList<BookResponse>> GetAllBooksAsync(int pageNumber, int pageSize, string searchTerm = "");
    
    Task<BookResponse> GetBookByIdAsync(Guid id);
    
    Task<BookResponse> CreateBookAsync(BookRequest bookRequest, string name);
    
    Task<BookResponse> UpdateBookAsync(Guid id, BookRequest bookRequest, string name);
    
    Task DeleteBookAsync(Guid id, string name );
}