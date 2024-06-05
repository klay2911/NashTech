using System.Security.Claims;
using LibraryManagement.Application.DTOs.CategoryDTOs;
using LibraryManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebAPI.Controllers;

[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private string Email => Convert.ToString(User.Claims.First(c => c.Type == ClaimTypes.Name).Value);

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllCategoryAsync(int pageNumber, int pageSize, string searchTerm = "")
    {
        var books = await _categoryService.GetAllCategoriesAsync(pageNumber, pageSize, searchTerm);
        return Ok(books);
    }
    

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(Guid id)
    {
        var book = await _categoryService.GetCategoryByIdAsync(id);
        return Ok(book);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBookAsync([FromBody] CategoryRequest categoryRequest)
    {
        var category = await _categoryService.CreateCategoryAsync(categoryRequest, Email);
        return CreatedAtAction(nameof(GetBookById), new { id = category.CategoryId }, category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBookAsync(Guid id, [FromBody] CategoryRequest categoryRequest)
    {
        var bookResponse = await _categoryService.UpdateCategoryAsync(id, categoryRequest, Email);
        return Ok(bookResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookAsync(Guid id)
    {
        await _categoryService.DeleteCategoryAsync(id, Email);
        return NoContent();
    }
}