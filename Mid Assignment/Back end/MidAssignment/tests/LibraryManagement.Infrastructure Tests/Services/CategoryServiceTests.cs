using LibraryManagement.Application.DTOs.CategoryDTOs;
using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Models;
using LibraryManagement.Infrastructure.Services;
using Moq;
using AutoMapper;
using LibraryManagement.Infrastructure.Repositories;

namespace LibraryManagement.Infrastructure_Tests.Services;

public class CategoryServiceTests
{
    private Mock<ICategoryRepository>? _mockCategoryRepository;
    private CategoryService? _service;
    private Mock<IMapper> _mockMapper;


    [SetUp]
    public void Setup()
    {
        _mockCategoryRepository = new Mock<ICategoryRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new CategoryService(_mockCategoryRepository.Object, _mockMapper.Object);
    }

    [Test]
    public async Task GetAllCategoriesAsync_WhenCalled_ReturnsAllCategories()
    {
        var categories = new List<Category>
        {
            new() { CategoryId = Guid.NewGuid(), Name = "Category1" },
            new() { CategoryId = Guid.NewGuid(), Name = "Category2" },
        };

        var categoryResponses = new List<CategoryResponse>
        {
            new() { CategoryId = categories[0].CategoryId, Name = categories[0].Name },
            new() { CategoryId = categories[1].CategoryId, Name = categories[1].Name },
        };

        _mockCategoryRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(categories.AsQueryable());

        _mockMapper.Setup(mapper => mapper.Map<CategoryResponse>(It.IsAny<Category>()))
            .Returns((Category category) => categoryResponses.First(response => response.CategoryId == category.CategoryId));

        var result = await _service.GetAllCategoriesAsync(1, 10);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Items.Count, Is.EqualTo(categories.Count));
        _mockCategoryRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
    }
    
    [Test]
    public async Task GetCategoryByIdAsync_WhenCalled_ReturnsCategory()
    {
        var category = new Category { CategoryId = Guid.NewGuid(), Name = "Category1" };
        var categoryResponse = new CategoryResponse { CategoryId = category.CategoryId, Name = category.Name };
        _mockCategoryRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(category);
        _mockMapper.Setup(mapper => mapper.Map<CategoryResponse>(It.IsAny<Category>())).Returns(categoryResponse);

        var result = await _service.GetCategoryByIdAsync(category.CategoryId);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(categoryResponse));
        _mockCategoryRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _mockMapper.Verify(mapper => mapper.Map<CategoryResponse>(It.IsAny<Category>()), Times.Once);
    }

    [Test]
    public async Task CreateCategoryAsync_WhenCalled_ReturnsCreatedCategory()
    {
        var categoryRequest = new CategoryRequest { Name = "Category1" };
        var category = new Category { CategoryId = Guid.NewGuid(), Name = categoryRequest.Name };
        var categoryResponse = new CategoryResponse { CategoryId = category.CategoryId, Name = category.Name };

        _mockMapper.Setup(mapper => mapper.Map<Category>(It.IsAny<CategoryRequest>())).Returns(category);
        _mockCategoryRepository.Setup(repo => repo.AddAsync(It.IsAny<Category>())).Returns(Task.CompletedTask);
        _mockMapper.Setup(mapper => mapper.Map<CategoryResponse>(It.IsAny<Category>())).Returns(categoryResponse);

        var result = await _service.CreateCategoryAsync(categoryRequest, "Test User");

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(categoryResponse));
        _mockCategoryRepository.Verify(repo => repo.AddAsync(It.IsAny<Category>()), Times.Once);
        _mockMapper.Verify(mapper => mapper.Map<CategoryResponse>(It.IsAny<Category>()), Times.Once);
    }

    [Test]
    public async Task UpdateCategoryAsync_WhenCalled_ReturnsUpdatedCategory()
    {
        var categoryRequest = new CategoryRequest { Name = "Category1" };
        var category = new Category { CategoryId = Guid.NewGuid(), Name = "Category2" };
        var categoryResponse = new CategoryResponse { CategoryId = category.CategoryId, Name = categoryRequest.Name };

        _mockCategoryRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(category);
        _mockCategoryRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Category>())).Returns(Task.CompletedTask);
        _mockMapper.Setup(mapper => mapper.Map<CategoryResponse>(It.IsAny<Category>())).Returns(categoryResponse);

        var result = await _service.UpdateCategoryAsync(category.CategoryId, categoryRequest, "Test User");

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(categoryResponse));
        _mockCategoryRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _mockCategoryRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Category>()), Times.Once);
        _mockMapper.Verify(mapper => mapper.Map<CategoryResponse>(It.IsAny<Category>()), Times.Once);
    }

    [Test]
    public async Task DeleteCategoryAsync_WhenCalled_DoesNotThrow()
    {
        var category = new Category { CategoryId = Guid.NewGuid(), Name = "Category1" };

        _mockCategoryRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(category);
        _mockCategoryRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Category>())).Returns(Task.CompletedTask);

        AsyncTestDelegate act = async () => await _service.DeleteCategoryAsync(category.CategoryId, "Test User");

        Assert.DoesNotThrowAsync(act);
        _mockCategoryRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _mockCategoryRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Category>()), Times.Once);
    }
}