using System.Security.Claims;
using LibraryManagement.Application.DTOs.CategoryDTOs;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.WebAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LibraryManagement.WebAPI_Tests.Controllers;

public class CategoryControllerTests
{
    private Mock<ICategoryService> _mockCategoryService;
    private CategoryController _controller;

    [SetUp]
    public void Setup()
    {
        _mockCategoryService = new Mock<ICategoryService>();
        _controller = new CategoryController(_mockCategoryService.Object);
    }

    [Test]
    public async Task GetAllCategoryAsync_CategoriesExist_ReturnsOkResult()
    {

        // Act
        var result = await _controller.GetAllCategoryAsync(1, 10, "");

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task GetBookById_CategoryExists_ReturnsOkResult()
    {
        // Arrange
        var categoryResponse = new CategoryResponse();
        _mockCategoryService.Setup(service => service.GetCategoryByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(categoryResponse);
        
        // Act
        var result = await _controller.GetCategoryById(Guid.NewGuid());

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task CreateBookAsync_ValidRequest_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var categoryRequest = new CategoryRequest();
        _mockCategoryService.Setup(service => service.CreateCategoryAsync(categoryRequest, It.IsAny<string>()))
            .ReturnsAsync(new CategoryResponse());
        // Create a ClaimsPrincipal
        var claims = new List<Claim> { new(ClaimTypes.Name, "test user") };
        var identity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(identity);

        // Assign it to the User property of the controller's ControllerContext
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = principal }
        };

        // Act
        var result = await _controller.CreateCategoryAsync(categoryRequest);

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result);
    }

    [Test]
    public async Task UpdateBookAsync_CategoryExists_ReturnsOkResult()
    {
        // Arrange
        var categoryRequest = new CategoryRequest();
        _mockCategoryService.Setup(service =>
                service.UpdateCategoryAsync(It.IsAny<Guid>(), categoryRequest, It.IsAny<string>()))
            .ReturnsAsync(new CategoryResponse());
        
        var claims = new List<Claim> { new(ClaimTypes.Name, "test user") };
        var identity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(identity);

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = principal }
        };
        var result = await _controller.UpdateCategoryAsync(Guid.NewGuid(), categoryRequest);

        Assert.IsInstanceOf<OkObjectResult>(result);
        Assert.IsNotNull(result);
    }
    
    

    [Test]
    public async Task DeleteBookAsync_CategoryExists_ReturnsNoContentResult()
    {
        var claims = new List<Claim> { new(ClaimTypes.Name, "test user") };
        var identity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(identity);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = principal }
        };

        var result = await _controller.DeleteCategoryAsync(Guid.NewGuid());

        Assert.IsInstanceOf<NoContentResult>(result);
    }
}