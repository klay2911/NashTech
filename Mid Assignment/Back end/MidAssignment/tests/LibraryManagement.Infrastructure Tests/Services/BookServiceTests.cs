using AutoMapper;
using LibraryManagement.Application.DTOs.BookDTOs;
using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Models;
using LibraryManagement.Infrastructure.Services;
using Moq;

namespace LibraryManagement.Infrastructure_Tests.Services;

public class BookServiceTests
{
    private Mock<IBookRepository>? _mockBookRepository;
    private BookService? _service;
    private Mock<IMapper> _mockMapper;


    [SetUp]
    public void Setup()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new BookService(_mockBookRepository.Object, _mockMapper.Object);
    }
    [Test]
    public async Task GetAllBooksAsync_WhenCalled_ReturnsAllBooks()
    {
        var books = new List<Book>
        {
            new() { Id = Guid.NewGuid(), Title = "Book1" },
            new() { Id = Guid.NewGuid(), Title = "Book2" },
        };

        var bookResponses = new List<BookResponse>
        {
            new() { Id = books[0].Id, Title = books[0].Title },
            new() { Id = books[1].Id, Title = books[1].Title },
        };

        _mockBookRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(books.AsQueryable());

        _mockMapper.Setup(mapper => mapper.Map<BookResponse>(It.IsAny<Book>()))
            .Returns((Book book) => bookResponses.First(response => response.Id == book.Id));

        // Act
        var result = await _service.GetAllBooksAsync(1, 10);

        // Assert
        Assert.That(result, Is.Not.Null);
        _mockBookRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Test]
    public async Task GetBookByIdAsync_WhenCalled_ReturnsBook()
    {
        var book = new Book { Id = Guid.NewGuid(), Title = "Book1" };
        var bookResponse = new BookResponse { Id = book.Id, Title = book.Title };

        _mockBookRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(book);
        _mockMapper.Setup(mapper => mapper.Map<BookResponse>(It.IsAny<Book>())).Returns(bookResponse);

        var result = await _service.GetBookByIdAsync(book.Id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(bookResponse));
        _mockBookRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _mockMapper.Verify(mapper => mapper.Map<BookResponse>(It.IsAny<Book>()), Times.Once);
    }

    [Test]
    public async Task CreateBookAsync_WhenCalled_ReturnsCreatedBook()
    {
        var bookRequest = new BookRequest { Title = "Book1" };
        var book = new Book { Id = Guid.NewGuid(), Title = bookRequest.Title };
        var bookResponse = new BookResponse { Id = book.Id, Title = book.Title };

        _mockMapper.Setup(mapper => mapper.Map<Book>(It.IsAny<BookRequest>())).Returns(book);
        _mockBookRepository!.Setup(repo => repo.AddAsync(It.IsAny<Book>())).Returns(Task.CompletedTask);
        _mockMapper.Setup(mapper => mapper.Map<BookResponse>(It.IsAny<Book>())).Returns(bookResponse);

        var result = await _service.CreateBookAsync(bookRequest, "Test User");

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(bookResponse));
        _mockBookRepository.Verify(repo => repo.AddAsync(It.IsAny<Book>()), Times.Once);
        _mockMapper.Verify(mapper => mapper.Map<BookResponse>(It.IsAny<Book>()), Times.Once);
    }

    [Test]
    public async Task UpdateBookAsync_WhenCalled_ReturnsUpdatedBook()
    {
        var bookRequest = new BookRequest { Title = "Book1" };
        var book = new Book { Id = Guid.NewGuid(), Title = "Book2" };
        var bookResponse = new BookResponse { Id = book.Id, Title = bookRequest.Title };

        _mockBookRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(book);
        _mockBookRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Book>())).Returns(Task.CompletedTask);
        _mockMapper.Setup(mapper => mapper.Map<BookResponse>(It.IsAny<Book>())).Returns(bookResponse);

        var result = await _service.UpdateBookAsync(book.Id, bookRequest, "Test User");

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(bookResponse));
        _mockBookRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _mockBookRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Book>()), Times.Once);
        _mockMapper.Verify(mapper => mapper.Map<BookResponse>(It.IsAny<Book>()), Times.Once);
    }

    [Test]
    public async Task DeleteBookAsync_WhenCalled_DoesNotThrow()
    {
        var book = new Book { Id = Guid.NewGuid(), Title = "Book1" };

        _mockBookRepository!.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(book);
        _mockBookRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Book>())).Returns(Task.CompletedTask);

        async Task Act() => await _service.DeleteBookAsync(book.Id, "Test User");

        Assert.DoesNotThrowAsync(Act);
        _mockBookRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _mockBookRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Book>()), Times.Once);
    }
}