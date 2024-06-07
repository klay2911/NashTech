using AutoMapper;
using LibraryManagement.Application.DTOs.BookDTOs;
using LibraryManagement.Application.DTOs.BorrowingRequestDTOs;
using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Enum;
using LibraryManagement.Domain.Models;
using LibraryManagement.Infrastructure.Services;
using Moq;

namespace LibraryManagement.Infrastructure_Tests.Services;

public class BookBorrowingServiceTests
{
    private Mock<IBookBorrowingRequestRepository>? _mockBookRequestRepository;
    private BookBorrowingService? _service;
    private Mock<IMapper> _mockMapper;
    private Mock<IBookRepository>? _mockBookRepository;
    private Mock<IBookBorrowingRequestDetailsRepository>? _mockBookRequestDetailRepository;

    [SetUp]
    public void Setup()
    {
        _mockBookRequestRepository = new Mock<IBookBorrowingRequestRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockBookRepository = new Mock<IBookRepository>();
        _mockBookRequestDetailRepository = new Mock<IBookBorrowingRequestDetailsRepository>();
        _service = new BookBorrowingService(_mockBookRequestRepository.Object, _mockBookRepository.Object,
            _mockBookRequestDetailRepository.Object, _mockMapper.Object);
    }

    [Test]
    public async Task GetRequests_WhenCalled_ReturnsAllRequests()
    {
        // Arrange
        var requests = new List<BookBorrowingRequest>
        {
            new() { RequestId = Guid.NewGuid(), DateRequested = DateTime.Now },
            new() { RequestId = Guid.NewGuid(), DateRequested = DateTime.Now }
        };

        var requestResponses = new List<BorrowingRequestResponse>
        {
            new() { RequestId = requests[0].RequestId, DateRequested = requests[0].DateRequested },
            new() { RequestId = requests[1].RequestId, DateRequested = requests[1].DateRequested }
        };

        _mockBookRequestRepository.Setup(repo => repo.GetAllWithUserAsync()).ReturnsAsync(requests.AsQueryable());

        _mockMapper.Setup(mapper => mapper.Map<BorrowingRequestResponse>(It.IsAny<BookBorrowingRequest>()))
            .Returns((BookBorrowingRequest request) =>
                requestResponses.First(response => response.RequestId == request.RequestId));

        // Act
        var result = await _service.GetRequests(1, 10);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Items.Count, Is.EqualTo(requests.Count));
        _mockBookRequestRepository.Verify(repo => repo.GetAllWithUserAsync(), Times.Once);
        _mockMapper.Verify(mapper => mapper.Map<BorrowingRequestResponse>(It.IsAny<BookBorrowingRequest>()),
            Times.Exactly(requests.Count));
    }

    [Test]
    public async Task GetRequestByIdAsync_WhenCalled_ReturnsRequest()
    {
        // Arrange
        var request = new BookBorrowingRequest { RequestId = Guid.NewGuid(), DateRequested = DateTime.Now };
        var requestResponse = new BorrowingRequestResponse
            { RequestId = request.RequestId, DateRequested = request.DateRequested };

        _mockBookRequestRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(request);
        _mockMapper.Setup(mapper => mapper.Map<BorrowingRequestResponse>(It.IsAny<BookBorrowingRequest>()))
            .Returns(requestResponse);

        // Act
        var result = await _service.GetRequestByIdAsync(request.RequestId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(requestResponse));
        _mockBookRequestRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _mockMapper.Verify(mapper => mapper.Map<BorrowingRequestResponse>(It.IsAny<BookBorrowingRequest>()),
            Times.Once);
    }

    [Test]
    public async Task RequestBorrowAsync_WhenCalled_ReturnsString()
    {
        // Arrange
        var bookIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var readerId = Guid.NewGuid();
        var email = "test@example.com";

        _mockBookRequestRepository.Setup(repo => repo.GetRequestsByUserAndMonthAsync(It.IsAny<Guid>(), It.IsAny<int>()))
            .ReturnsAsync(new List<BookBorrowingRequest>());
        _mockBookRequestRepository.Setup(repo => repo.AddAsync(It.IsAny<BookBorrowingRequest>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.RequestBorrowAsync(readerId, email, bookIds);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo($"Request for {bookIds.Count} books has been submitted successfully."));
        _mockBookRequestRepository.Verify(repo => repo.AddAsync(It.IsAny<BookBorrowingRequest>()), Times.Once);
    }

    [Test]
    public async Task ManageBorrowingRequest_WhenCalled_ReturnsBool()
    {
        // Arrange
        var requestId = Guid.NewGuid();
        var librarianId = Guid.NewGuid();
        var email = "test@example.com";
        var status = RequestStatus.Approved;

        var request = new BookBorrowingRequest { RequestId = requestId, DateRequested = DateTime.Now };

        _mockBookRequestRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(request);
        _mockBookRequestRepository.Setup(repo => repo.UpdateAsync(It.IsAny<BookBorrowingRequest>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.ManageBorrowingRequest(librarianId, email, requestId, status);

        // Assert
        Assert.That(result, Is.True);
        _mockBookRequestRepository.Verify(repo => repo.UpdateAsync(It.IsAny<BookBorrowingRequest>()), Times.Once);
    }

    [Test]
    public async Task GetRequestByIdAsync_WhenCalledWithNonExistingId_ReturnsNull()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        _mockBookRequestRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((BookBorrowingRequest)null);

        // Act
        var result = await _service.GetRequestByIdAsync(nonExistingId);

        // Assert
        Assert.That(result, Is.Null);
        _mockBookRequestRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Test]
    public async Task GetRequests_WhenCalledWithSearchTerm_FiltersRequests()
    {
        var searchTerm = "John";

        var requests = new List<BookBorrowingRequest>
        {
            new() { User = new User { FirstName = "John", LastName = "Doe" }, Status = RequestStatus.Waiting },
            new() { User = new User { FirstName = "Jane", LastName = "Doe" }, Status = RequestStatus.Approved }
        };

        _mockBookRequestRepository.Setup(repo => repo.GetAllWithUserAsync()).ReturnsAsync(requests.AsQueryable());

        var result = await _service.GetRequests(1, 10, searchTerm);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Items.Count, Is.EqualTo(1));
        _mockBookRequestRepository.Verify(repo => repo.GetAllWithUserAsync(), Times.Once);
    }


    [Test]
    public async Task GetUserBorrowedBooks_WhenCalled_ReturnsBorrowedBooks()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var pageNumber = 1;
        var pageSize = 10;
        var searchTerm = "";

        var book1Id = Guid.NewGuid();
        var book2Id = Guid.NewGuid();

        var borrowingRequests = new List<BookBorrowingRequest>
        {
            new()
            {
                RequestorId = userId,
                BookBorrowingRequestDetails = new List<BookBorrowingRequestDetails>
                {
                    new() { BookId = book1Id },
                    new() { BookId = book2Id }
                }
            }
        };

        var borrowedBooks = new List<BookResponse>
        {
            new() { Id = book1Id, Title = "Book1" },
            new() { Id = book2Id, Title = "Book2" }
        };

        _mockBookRequestRepository.Setup(repo => repo.GetAllRequestsByUserAsync(It.IsAny<Guid>()))
            .ReturnsAsync(borrowingRequests);

        _mockBookRepository.Setup(repo => repo.GetByIdAsync(book1Id))
            .ReturnsAsync(new Book { Id = book1Id, Title = "Book1" });
        _mockBookRepository.Setup(repo => repo.GetByIdAsync(book2Id))
            .ReturnsAsync(new Book { Id = book2Id, Title = "Book2" });

        _mockMapper.Setup(mapper => mapper.Map<BookResponse>(It.IsAny<Book>()))
            .Returns((Book book) => borrowedBooks.First(b => b.Id == book.Id));

        // Act
        var result = await _service.GetUserBorrowedBooks(userId, pageNumber, pageSize, searchTerm);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Items.Count, Is.EqualTo(borrowedBooks.Count));
        _mockBookRequestRepository.Verify(repo => repo.GetAllRequestsByUserAsync(It.IsAny<Guid>()), Times.Once);
        _mockBookRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Exactly(borrowedBooks.Count));
        _mockMapper.Verify(mapper => mapper.Map<BookResponse>(It.IsAny<Book>()), Times.Exactly(borrowedBooks.Count));
    }
}