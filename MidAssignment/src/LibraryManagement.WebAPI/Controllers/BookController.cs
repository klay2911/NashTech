using LibraryManagement.Application.DTOs.BookDTOs;
using LibraryManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllBooks(int pageNumber, int pageSize, string searchTerm = "")
    {
        var books = await _bookService.GetAllBooksAsync(pageNumber, pageSize, searchTerm);
        return Ok(books);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(Guid id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromForm] BookRequest bookRequest)
    {
        var bookResponse = await _bookService.CreateBookAsync(bookRequest);
        return CreatedAtAction(nameof(GetBookById), new { id = bookResponse.Id }, bookResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(Guid id, [FromForm] BookRequest bookRequest)
    {
        var bookResponse = await _bookService.UpdateBookAsync(id, bookRequest);
        return Ok(bookResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        await _bookService.DeleteBookAsync(id);
        return NoContent();
    }
}