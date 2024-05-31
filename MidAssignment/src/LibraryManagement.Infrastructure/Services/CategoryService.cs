using AutoMapper;
using LibraryManagement.Application.Common.Models;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Infrastructure.Services;

// public class CategoryService : ICategoryService
// {
//     private readonly ICategoryRepository _categoryRepository;
//     private readonly IMapper _mapper;
//
//     public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
//     {
//         _categoryRepository = categoryRepository;
//         _mapper = mapper;
//     }
//     public async Task<PaginatedList<CategoryResponse>> GetAllBooksAsync(int pageNumber, int pageSize, string searchTerm = "")
//     {
//         var books = await _categoryRepository.();
//
//         if (!string.IsNullOrEmpty(searchTerm))
//         {
//             books = books.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm));
//         }
//
//         var bookResponses = books.Select(book => _mapper.Map<BookResponse>(book));
//
//         var pagedBookResponses = PaginatedList<BookResponse>.Create(bookResponses, pageNumber, pageSize);
//
//         return await Task.FromResult(pagedBookResponses);
//     }
//     
//     public async Task<BookResponse> GetBookByIdAsync(Guid id)
//     {
//         var book = await _bookRepository.GetByIdAsync(id);
//         if (book == null)
//         {
//             throw new Exception("Book not found");
//         }
//         return _mapper.Map<BookResponse>(book);
//     }
//     
//     public async Task<BookResponse> CreateBookAsync(BookRequest bookRequest)
//     {
//         var book =  _mapper.Map<Book>(bookRequest);
//         await Task.Run(() =>_bookRepository.AddAsync(book));
//         
//         return _mapper.Map<BookResponse>(book);
//     }
//
//     public async Task<BookResponse> UpdateBookAsync(Guid id, BookRequest bookRequest)
//     {
//         var book = await _bookRepository.GetByIdAsync(id);
//         if (book == null)
//         {
//             throw new Exception("Book not found");
//         }
//
//         _mapper.Map(bookRequest, book);
//         await Task.Run(() =>_bookRepository.UpdateAsync(book));
//
//         return _mapper.Map<BookResponse>(book);
//     }
//
//     public async Task DeleteBookAsync(Guid id)
//     {
//         var book = await _bookRepository.GetByIdAsync(id);
//         if (book == null)
//         {
//             throw new Exception("Book not found");
//         }
//
//         await Task.Run(() =>_bookRepository.DeleteAsync(book));
//     }
// }