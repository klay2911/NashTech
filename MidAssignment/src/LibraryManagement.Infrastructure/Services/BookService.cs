using System.Linq.Expressions;
using AutoMapper;
using LibraryManagement.Application.Common.Models;
using LibraryManagement.Application.DTOs.BookDTOs;
using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
    
    public async Task<PaginatedList<BookResponse>> GetFilterAsync(FilterRequest request)
    {
        var query = _bookRepository.GetBooksQuery();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(p =>
                p.Title.Contains(request.SearchTerm) ||
                p.Author.Contains(request.SearchTerm));
        }

        if (request.SortOrder?.ToLower() == "desc")
        {
            query = query.OrderByDescending(GetSortProperty(request));
        }
        else
        {
            query = query.OrderBy(GetSortProperty(request));
        }
        var items = await query.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
        var bookResponses = _mapper.Map<List<BookResponse>>(items);
        //maybe wrong
        return PaginatedList<BookResponse>.Create(bookResponses, request.Page, request.PageSize);
    }

    private static Expression<Func<Book, object>> GetSortProperty(FilterRequest request) =>
        request.SortColumn?.ToLower() switch
        {
            "title" => product => product.Title,
            "author" => product => product.Author,
            _ => product => product.ModifyAt
        };
    
    public async Task<BookResponse> GetBookByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            throw new Exception("Book not found");
        }
        return _mapper.Map<BookResponse>(book);
    }
    
    public async Task<BookResponse> CreateBookAsync(BookRequest bookRequest, string name)
    {
        var book =  _mapper.Map<Book>(bookRequest);
        if(book is BaseModel baseModel)
        {
            baseModel.CreatedAt = DateTime.Now;
            baseModel.CreatedBy = name;
        }
        await _bookRepository.AddAsync(book);
        return _mapper.Map<BookResponse>(book);
    }

    public async Task<BookResponse> UpdateBookAsync(Guid id, BookRequest bookRequest, string name )
    {
        var book = await _bookRepository.GetByIdAsync(id);
        switch (book)
        {
            case BaseModel baseModel:
                baseModel.ModifyAt = DateTime.Now;
                baseModel.ModifyBy = name;
                break;
            case null:
                throw new Exception("Book not found");
        }

        _mapper.Map(bookRequest, book);
        await _bookRepository.UpdateAsync(book);

        return _mapper.Map<BookResponse>(book);
    }

    public async Task DeleteBookAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            throw new Exception("Book not found");
        }

        await _bookRepository.DeleteAsync(book);
    }
}