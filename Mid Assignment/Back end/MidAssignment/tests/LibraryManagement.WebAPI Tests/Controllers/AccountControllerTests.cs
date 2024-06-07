using LibraryManagement.Application.DTOs.AuthDTOs;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LibraryManagement.WebAPI_Tests.Controllers;

[TestFixture]
public class AccountControllerTests
{
    private Mock<IUserService> _mockUserService;
    private AccountController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockUserService = new Mock<IUserService>();
        _controller = new AccountController(_mockUserService.Object);
    }

    [Test]
    public async Task LoginAsync_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Email = "test@example.com",
            Password = "Test@123"
        };

        _mockUserService
            .Setup(service => service.LoginAsync(It.IsAny<LoginRequest>()))
            .ReturnsAsync(new LoginResponse(true,"loginSuccess", "token"));

        // Act
        var result = await _controller.LoginAsync(loginRequest);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
    }

    [Test]
    public async Task RegisterAsync_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        var registerRequest = new RegisterRequest
        {
            Email = "test@example.com",
            Password = "Test@123",
            ConfirmPassword = "Test@123",
            FirstName = "Test",
            LastName = "User"
        };

        _mockUserService
            .Setup(service => service.RegisterAsync(It.IsAny<RegisterRequest>()))
            .ReturnsAsync(new RegisterResponse(true, "registerSuccess"));

        // Act
        var result = await _controller.RegisterAsync(registerRequest);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
    }
}