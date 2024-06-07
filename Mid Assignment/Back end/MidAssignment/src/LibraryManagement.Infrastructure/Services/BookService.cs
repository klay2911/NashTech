using System.Linq.Expressions;
using AutoMapper;
using LibraryManagement.Application.Common.Models;
using LibraryManagement.Application.DTOs.BookDTOs;
using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public BookService(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BookResponse>> GetAllBooksAsync(int pageNumber, int pageSize,
        string searchTerm = "")
    {
        var books = await _bookRepository.GetAllAsync();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            books = books.Where(b =>
                b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm) || b.Category.Name.Contains(searchTerm));
        }

        var bookResponses = _mapper.Map<IEnumerable<BookResponse>>(books);

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

    public async Task<BookResponse> CreateBookAsync(BookRequest bookRequest, string name)
    {
        var book = _mapper.Map<Book>(bookRequest);
        if (book is BaseModel baseModel)
        {
            baseModel.CreatedAt = DateTime.Now;
            baseModel.CreatedBy = name;
        }

        await _bookRepository.AddAsync(book);
        return _mapper.Map<BookResponse>(book);
    }

    public async Task<BookResponse> UpdateBookAsync(Guid id, BookRequest bookRequest, string name)
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

    public async Task DeleteBookAsync(Guid id, string name)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            throw new Exception("Book not found");
        }

        book.ModifyAt = DateTime.Now;
        book.ModifyBy = name;
        book.IsDeleted = true;
        await _bookRepository.UpdateAsync(book);
    }
}