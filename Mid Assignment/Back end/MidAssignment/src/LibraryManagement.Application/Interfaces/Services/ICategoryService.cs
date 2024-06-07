using LibraryManagement.Application.Common.Models;
using LibraryManagement.Application.DTOs.CategoryDTOs;

namespace LibraryManagement.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<PaginatedList<CategoryResponse>> GetAllCategoriesAsync(int pageNumber, int pageSize, string searchTerm = "");

    Task<CategoryResponse> GetCategoryByIdAsync(Guid id);

    Task<CategoryResponse> CreateCategoryAsync(CategoryRequest categoryRequest, string name);
    
    Task<CategoryResponse> UpdateCategoryAsync(Guid id, CategoryRequest categoryRequest, string name);

    Task DeleteCategoryAsync(Guid id, string name);
}