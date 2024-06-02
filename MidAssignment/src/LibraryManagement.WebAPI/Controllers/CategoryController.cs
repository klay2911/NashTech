using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebAPI.Controllers;

[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
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
    public async Task<IActionResult> CreateBookAsync([FromForm] CategoryRequest categoryRequest)
    {
        var category = await _categoryService.CreateCategoryAsync(categoryRequest);
        return CreatedAtAction(nameof(GetBookById), new { id = category.CategoryId }, category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBookAsync(Guid id, [FromForm] CategoryRequest bookRequest, string name)
    {
        var bookResponse = await _categoryService.UpdateCategoryAsync(id, bookRequest);
        return Ok(bookResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookAsync(Guid id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }
}