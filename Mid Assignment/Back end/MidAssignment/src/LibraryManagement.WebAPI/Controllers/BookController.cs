using System.Security.Claims;
using LibraryManagement.Application.DTOs.BookDTOs;
using LibraryManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Domain.Enum;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.WebAPI.Controllers;

[Route("api/books")]
[ApiController]
[Authorize]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    private string Email => Convert.ToString(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value);
    
    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }
    
    [HttpGet]
    [Authorize(Roles = nameof(Role.Librarian) + "," + nameof(Role.Reader))]
    public async Task<IActionResult> GetAllBooksAsync(int pageNumber, int pageSize, string searchTerm = "")
    {
        var books = await _bookService.GetAllBooksAsync(pageNumber, pageSize, searchTerm);
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookByIdAsync(Guid id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        return Ok(book);
    }
    
    [HttpPost]
    [Authorize(Roles = nameof(Role.Librarian))]
    public async Task<IActionResult> CreateBookAsync([FromForm] BookRequest bookRequest, IFormFile pdfFile, IFormFile coverFile)
    {
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdfs");
        var coverDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "covers");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        if (!Directory.Exists(coverDirectoryPath))
        {
            Directory.CreateDirectory(coverDirectoryPath);
        }
        
        var filePath = Path.Combine(directoryPath, pdfFile.FileName);
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await pdfFile.CopyToAsync(stream);
        }
        bookRequest.BookPath = $"/pdfs/{pdfFile.FileName}";
        
        var coverFilePath = Path.Combine(coverDirectoryPath, coverFile.FileName);
        await using (var stream = new FileStream(coverFilePath, FileMode.Create))
        {
            await coverFile.CopyToAsync(stream);
        }
        bookRequest.CoverPath = $"/covers/{coverFile.FileName}";

        var bookResponse = await _bookService.CreateBookAsync(bookRequest, Email);
        return Ok(bookResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBookAsync(Guid id, [FromForm] BookRequest bookRequest,IFormFile pdfFile, IFormFile coverFile)
    {
        var oldBook = await _bookService.GetBookByIdAsync(id);
        if (oldBook == null)
        {
            return NotFound();
        }
        if (pdfFile is { Length: > 0 })
        {
            if (!string.IsNullOrEmpty(oldBook.BookPath))
            {
                var oldPdfPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", oldBook.BookPath.TrimStart('/'));
                if (System.IO.File.Exists(oldPdfPath))
                {
                    System.IO.File.Delete(oldPdfPath);
                }
            }
        
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdfs");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        
            var filePath = Path.Combine(directoryPath, pdfFile.FileName);
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await pdfFile.CopyToAsync(stream);
            }
        
            bookRequest.BookPath = $"/pdfs/{pdfFile.FileName}";
        }
        else
        {
            bookRequest.BookPath = oldBook.BookPath;
        }
        if (coverFile is { Length: > 0 })
        {
            if (!string.IsNullOrEmpty(oldBook.CoverPath))
            {
                var oldCoverPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", oldBook.CoverPath.TrimStart('/'));
                if (System.IO.File.Exists(oldCoverPath))
                {
                    System.IO.File.Delete(oldCoverPath);
                }
            }

            var coverDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "covers");
            if (!Directory.Exists(coverDirectoryPath))
            {
                Directory.CreateDirectory(coverDirectoryPath);
            }

            var coverFilePath = Path.Combine(coverDirectoryPath, coverFile.FileName);
            await using (var stream = new FileStream(coverFilePath, FileMode.Create))
            {
                await coverFile.CopyToAsync(stream);
            }

            bookRequest.CoverPath = $"/covers/{coverFile.FileName}";
        }
        else
        {
            bookRequest.CoverPath = oldBook.CoverPath;
        }

        var bookResponse = await _bookService.UpdateBookAsync(id, bookRequest, Email);
        return Ok(bookResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookAsync(Guid id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (!string.IsNullOrEmpty(book.BookPath))
        {
            var pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", book.BookPath.TrimStart('/'));
            if (System.IO.File.Exists(pdfPath))
            {
                System.IO.File.Delete(pdfPath);
            }
        }
        await _bookService.DeleteBookAsync(id, Email);
        return NoContent();
    }
}