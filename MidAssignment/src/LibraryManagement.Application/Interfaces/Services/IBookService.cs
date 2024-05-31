using LibraryManagement.Application.Common.Models;
using LibraryManagement.Application.DTOs.BookDTOs;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;

namespace LibraryManagement.Application.Interfaces.Services;

public interface IBookService
{
    Task<PaginatedList<BookResponse>> GetAllBooksAsync(int pageNumber, int pageSize, string searchTerm = "");
    
    Task<BookResponse> GetBookByIdAsync(Guid id);
    
    Task<BookResponse> CreateBookAsync(BookRequest bookRequest);
    
    Task<BookResponse> UpdateBookAsync(Guid id, BookRequest bookRequest);
    
    Task DeleteBookAsync(Guid id);
}