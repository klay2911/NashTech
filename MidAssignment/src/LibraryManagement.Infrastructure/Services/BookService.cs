using AutoMapper;
using LibraryManagement.Application.Common.Models;
using LibraryManagement.Application.DTOs.BookDTOs;
using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Infrastructure.Services;

public class BookService: IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;


    public BookService(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    // public async Task<PaginatedList<BookResponse>> GetAllBooksAsync(int pageNumber, int pageSize, string searchTerm = "")
    // {
    //     var books = await _bookRepository.GetAllAsync(); 
    //
    //     if (!string.IsNullOrEmpty(searchTerm))
    //     {
    //         books = books.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm));
    //     }
    //
    //     var bookResponses = books.Select(book => _mapper.Map<BookResponse>(book));
    //
    //     return await PaginatedList<BookResponse>.CreateAsync(bookResponses.AsQueryable(), pageNumber, pageSize);
    // }
    public async Task<PaginatedList<BookResponse>> GetAllBooksAsync(int pageNumber, int pageSize, string searchTerm = "")
    {
        var books = await _bookRepository.GetAllAsync();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            books = books.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm));
        }

        var bookResponses = books.Select(book => _mapper.Map<BookResponse>(book));

        var pagedBookResponses = PaginatedList<BookResponse>.Create(bookResponses, pageNumber, pageSize);

        return await Task.FromResult(pagedBookResponses);
    }
    
    public async Task<BookResponse> GetBookByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            throw new Exception("Book not found");
        }
        return _mapper.Map<BookResponse>(book);
    }
    
    public async Task<BookResponse> CreateBookAsync(BookRequest bookRequest)
    {
        var book =  _mapper.Map<Book>(bookRequest);
        await Task.Run(() =>_bookRepository.AddAsync(book));
        
        return _mapper.Map<BookResponse>(book);
    }

    public async Task<BookResponse> UpdateBookAsync(Guid id, BookRequest bookRequest)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            throw new Exception("Book not found");
        }

        _mapper.Map(bookRequest, book);
        await Task.Run(() =>_bookRepository.UpdateAsync(book));

        return _mapper.Map<BookResponse>(book);
    }

    public async Task DeleteBookAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            throw new Exception("Book not found");
        }

        await Task.Run(() =>_bookRepository.DeleteAsync(book));
    }
}