using System.Security.Claims;
using System.Text;
using LibraryManagement.Application.Common.Models;
using LibraryManagement.Application.DTOs.BookDTOs;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.WebAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LibraryManagement.WebAPI_Tests.Controllers;

[TestFixture]
public class BookControllerTests
{
    private Mock<IBookService> _mockBookService;
    private BookController _controller;

    [SetUp]
    public void Setup()
    {
        _mockBookService = new Mock<IBookService>();
        _controller = new BookController(_mockBookService.Object);
    }

    [Test]
    public async Task GetAllBooksAsync_ReturnsOkResult()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 10;
        var searchTerm = "test";
        var bookList = new List<BookResponse>(); // Create a list of BookResponse
        _mockBookService.Setup(service => service.GetAllBooksAsync(pageNumber, pageSize, searchTerm))
            .ReturnsAsync(new PaginatedList<BookResponse>(bookList, bookList.Count, pageNumber, pageSize)); 

        // Act
        var result = await _controller.GetAllBooksAsync(pageNumber, pageSize, searchTerm);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task GetBookByIdAsync_BookExists_ReturnsOkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockBookService.Setup(service => service.GetBookByIdAsync(id))
            .ReturnsAsync(new BookResponse());

        // Act
        var result = await _controller.GetBookByIdAsync(id);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task CreateBookAsync_ValidRequest_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var bookRequest = new BookRequest();
        _mockBookService.Setup(service => service.CreateBookAsync(bookRequest, It.IsAny<string>()))
            .ReturnsAsync(new BookResponse());

        var mockPdfFile = new Mock<IFormFile>();
        mockPdfFile.Setup(file => file.FileName).Returns("test.pdf");

        var mockCoverFile = new Mock<IFormFile>();
        mockCoverFile.Setup(file => file.FileName).Returns("test.png");

        // Mock User
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "test@example.com")
        };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var principal = new ClaimsPrincipal(identity);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = principal }
        };

        // Act
        var result = await _controller.CreateBookAsync(bookRequest, mockPdfFile.Object, mockCoverFile.Object);

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result);
    }

    [Test]
    public async Task UpdateBookAsync_ValidRequest_UpdatesBook()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var bookRequest = new BookRequest { Title = "New Title" };
        var oldBook = new BookResponse { Id = bookId, Title = "Old Title" };
        var updatedBook = new BookResponse { Id = bookId, Title = "New Title" };
        var pdfFile = new FormFile(new MemoryStream("This is a dummy file"u8.ToArray()), 0, 0, "Data", "dummy.pdf");
        var coverFile = new FormFile(new MemoryStream("This is a dummy file"u8.ToArray()), 0, 0, "Data", "dummy.jpg");

        _mockBookService.Setup(service => service.GetBookByIdAsync(bookId)).ReturnsAsync(oldBook);
        _mockBookService.Setup(service => service.UpdateBookAsync(bookId, bookRequest, It.IsAny<string>())).ReturnsAsync(updatedBook);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "username")
        }));

        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await _controller.UpdateBookAsync(bookId, bookRequest, pdfFile, coverFile);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var updatedBookResult = (result as OkObjectResult).Value as BookResponse;
        Assert.AreEqual(updatedBook.Title, updatedBookResult.Title);
    }

    [Test]
    public async Task UpdateBookAsync_BookExists_ReturnsOkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var bookRequest = new BookRequest();

        // Setup _mockBookService to return a valid BookResponse for GetBookByIdAsync
        _mockBookService.Setup(service => service.GetBookByIdAsync(id))
            .ReturnsAsync(new BookResponse { Id = id, BookPath = "old/path", CoverPath = "old/cover/path" });

        _mockBookService.Setup(service => service.UpdateBookAsync(id, bookRequest, It.IsAny<string>()))
            .ReturnsAsync(new BookResponse());
    
        var mockPdfFile = new Mock<IFormFile>();
        mockPdfFile.Setup(file => file.FileName).Returns("test.pdf");

        var mockCoverFile = new Mock<IFormFile>();
        mockCoverFile.Setup(file => file.FileName).Returns("test.png");
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "test@example.com")
        };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var principal = new ClaimsPrincipal(identity);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = principal }
        };
    
        // Act
        var result = await _controller.UpdateBookAsync(id, bookRequest, mockPdfFile.Object, mockCoverFile.Object);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }
    
    [Test]
    public async Task UpdateBookAsync_BookNotExists_ReturnsNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var bookRequest = new BookRequest();
        _mockBookService.Setup(service => service.UpdateBookAsync(id, bookRequest, It.IsAny<string>()))
            .ReturnsAsync(new BookResponse());
        
        var mockPdfFile = new Mock<IFormFile>();
        mockPdfFile.Setup(file => file.FileName).Returns("test.pdf");

        var mockCoverFile = new Mock<IFormFile>();
        mockCoverFile.Setup(file => file.FileName).Returns("test.png");
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "test@example.com")
        };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var principal = new ClaimsPrincipal(identity);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = principal }
        };
        
        // Act
        var result = await _controller.UpdateBookAsync(id, bookRequest, mockPdfFile.Object, mockCoverFile.Object);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task DeleteBookAsync_BookExists_ReturnsNoContentResult()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Setup _mockBookService to return a valid BookResponse for GetBookByIdAsync
        _mockBookService.Setup(service => service.GetBookByIdAsync(id))
            .ReturnsAsync(new BookResponse { Id = id, BookPath = "old/path", CoverPath = "old/cover/path" });

        _mockBookService.Setup(service => service.DeleteBookAsync(id, It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "test@example.com")
        };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var principal = new ClaimsPrincipal(identity);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = principal }
        };

        // Act
        var result = await _controller.DeleteBookAsync(id);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
    }
    // [Test]
    // public async Task UpdateBookAsync_OldPdfExists_DeletesOldPdf()
    // {
    //     // Arrange
    //     var bookId = Guid.NewGuid();
    //     var bookRequest = new BookRequest { Title = "New Title" };
    //     var oldBook = new BookResponse { Id = bookId, Title = "Old Title", BookPath = "/pdfs/old.pdf" };
    //     var updatedBook = new BookResponse { Id = bookId, Title = "New Title" };
    //     var pdfFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.pdf");
    //     var coverFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.jpg");
    //
    //     _mockBookService.Setup(service => service.GetBookByIdAsync(bookId)).ReturnsAsync(oldBook);
    //     _mockBookService.Setup(service => service.UpdateBookAsync(bookId, bookRequest, It.IsAny<string>())).ReturnsAsync(updatedBook);
    //
    //     var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
    //     {
    //         new Claim(ClaimTypes.Name, "username")
    //     }));
    //
    //     _controller.ControllerContext = new ControllerContext()
    //     {
    //         HttpContext = new DefaultHttpContext() { User = user }
    //     };
    //
    //     // Create old PDF file
    //     var oldPdfPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", oldBook.BookPath.TrimStart('/'));
    //     Directory.CreateDirectory(Path.GetDirectoryName(oldPdfPath));
    //     await File.WriteAllTextAsync(oldPdfPath, "This is a dummy file");
    //
    //     // Act
    //     await _controller.UpdateBookAsync(bookId, bookRequest, pdfFile, coverFile);
    //
    //     // Assert
    //     Assert.IsFalse(File.Exists(oldPdfPath));
    // }
}