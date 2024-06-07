using System.Security.Claims;
using LibraryManagement.Application.DTOs.CategoryDTOs;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebAPI.Controllers;

[Route("api/categories")]
[ApiController]
[Authorize(Roles = nameof(Role.Librarian))]
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
        var categories = await _categoryService.GetAllCategoriesAsync(pageNumber, pageSize, searchTerm);
        return Ok(categories);
    }
    

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        return Ok(category);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryRequest categoryRequest)
    {
        var category = await _categoryService.CreateCategoryAsync(categoryRequest, Email);
        return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategoryAsync(Guid id, [FromBody] CategoryRequest categoryRequest)
    {
        var categoryResponse = await _categoryService.UpdateCategoryAsync(id, categoryRequest, Email);
        return Ok(categoryResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoryAsync(Guid id)
    {
        await _categoryService.DeleteCategoryAsync(id, Email);
        return NoContent();
    }
}