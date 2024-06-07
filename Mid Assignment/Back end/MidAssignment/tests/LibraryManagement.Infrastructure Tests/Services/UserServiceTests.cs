using LibraryManagement.Application.DTOs.AuthDTOs;
using LibraryManagement.Application.Interfaces.Helpers;
using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.Domain.Models;
using LibraryManagement.Infrastructure.Services;
using Moq;

namespace LibraryManagement.Infrastructure_Tests.Services;

public class UserServiceTests
{
    private Mock<IUserRepository> _mockUserRepository;
    private Mock<ITokenService> _mockTokenService;
    private Mock<IAuthHelper> _mockAuthHelper;
    private UserService _userService;

    [SetUp]
    public void Setup()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockTokenService = new Mock<ITokenService>();
        _mockAuthHelper = new Mock<IAuthHelper>();
        _userService = new UserService(_mockUserRepository.Object, _mockTokenService.Object, _mockAuthHelper.Object);
    }

    [Test]
    public async Task GetByIdAsync_UserExists_ReturnsUser()
    {
        // Arrange
        var id = Guid.NewGuid();
        var user = new User { UserId = id, Email = "test@example.com" };
        _mockUserRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(user);

        // Act
        var result = await _userService.GetByIdAsync(id);

        // Assert
        Assert.AreEqual(id, result.UserId);
        Assert.AreEqual("test@example.com", result.Email);
    }

    [Test]
    public async Task RegisterAsync_ValidRequest_ReturnsRegisterResponse()
    {
        // Arrange
        var registerRequest = new RegisterRequest { Email = "test@example.com", Password = "password", FirstName = "Test" };
        _mockUserRepository.Setup(repo => repo.FindUserByEmailAsync(registerRequest.Email)).ReturnsAsync((User)null);
        _mockAuthHelper.Setup(helper => helper.HashPassword(registerRequest.Password)).Returns("hashedpassword");
        _mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
        _mockUserRepository.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _userService.RegisterAsync(registerRequest);

        // Assert
        Assert.IsTrue(result.Message == "Register success");
        Assert.AreEqual("Register success", result.Message);
    }
}