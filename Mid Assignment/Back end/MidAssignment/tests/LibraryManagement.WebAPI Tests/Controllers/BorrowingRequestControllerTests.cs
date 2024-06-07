using System.Security.Claims;
using LibraryManagement.Application.DTOs.BorrowingRequestDTOs;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.Domain.Enum;
using LibraryManagement.WebAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LibraryManagement.WebAPI_Tests.Controllers;

public class BorrowingRequestControllerTests
{
    private Mock<IBookBorrowingRequestService> _mockBorrowingRequestService;
        private BorrowingRequestController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockBorrowingRequestService = new Mock<IBookBorrowingRequestService>();
            _controller = new BorrowingRequestController(_mockBorrowingRequestService.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "test@example.com"),
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Test]
        public async Task CreateBorrowRequestAsync_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var bookIds = new List<Guid> { Guid.NewGuid() };

            _mockBorrowingRequestService
                .Setup(service => service.RequestBorrowAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<List<Guid>>()))
                .ReturnsAsync("Request submitted successfully.");

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()), // Use a valid Guid string
                new Claim(ClaimTypes.Name, "test@example.com"),
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await _controller.CreateBorrowRequestAsync(bookIds);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        // [Test]
        // public async Task GetBorrowRequestsAsync_ValidRequest_ReturnsOkResult()
        // {
        //     // Arrange
        //     _mockBorrowingRequestService
        //         .Setup(service => service.GetWaitingRequests(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
        //         .ReturnsAsync(new PaginatedList<BorrowingRequestResponse>());
        //
        //     // Act
        //     var result = await _controller.GetBorrowRequestsAsync(1, 10, "test");
        //
        //     // Assert
        //     Assert.IsInstanceOf<OkObjectResult>(result);
        // }

        [Test]
        public async Task GetBorrowRequestByIdAsync_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var requestId = Guid.NewGuid();

            _mockBorrowingRequestService
                .Setup(service => service.GetRequestByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new BorrowingRequestResponse());

            // Act
            var result = await _controller.GetBorrowRequestByIdAsync(requestId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        // [Test]
        // public async Task GetUserBorrowedBooksAsync_ValidRequest_ReturnsOkResult()
        // {
        //     // Arrange
        //     var userId = Guid.NewGuid();
        //
        //     _mockBorrowingRequestService
        //         .Setup(service => service.GetUserBorrowedBooks(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
        //         .ReturnsAsync(new PaginatedList<BookResponse>( ,10, 10, 5));
        //
        //     // Act
        //     var result = await _controller.GetUserBorrowedBooksAsync(userId, 1, 10, "test");
        //
        //     // Assert
        //     Assert.IsInstanceOf<OkObjectResult>(result);
        // }

        [Test]
        public async Task UpdateBorrowRequestStatusAsync_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var requestId = Guid.NewGuid();

            _mockBorrowingRequestService
                .Setup(service => service.ManageBorrowingRequest(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<RequestStatus>()))
                .ReturnsAsync(true);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()), // Use a valid Guid string
                new Claim(ClaimTypes.Name, "test@example.com"),
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await _controller.UpdateBorrowRequestStatusAsync(requestId, RequestStatus.Approved);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
}