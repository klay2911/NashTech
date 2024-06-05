using AutoMapper;
using LibraryManagement.Application.Common.Models;
using LibraryManagement.Application.DTOs.BookDTOs;
using LibraryManagement.Application.DTOs.CategoryDTOs;
using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    public async Task<PaginatedList<CategoryResponse>> GetAllCategoriesAsync(int pageNumber, int pageSize, string searchTerm = "")
    {
        var categories = await _categoryRepository.GetAllAsync();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            categories = categories.Where(c => c.Name.Contains(searchTerm));
        }

        var categoryResponses = categories.Select(category => _mapper.Map<CategoryResponse>(category));

        var pagedCategoryResponses = PaginatedList<CategoryResponse>.Create(categoryResponses, pageNumber, pageSize);

        return await Task.FromResult(pagedCategoryResponses);
    }

    public async Task<CategoryResponse> GetCategoryByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            throw new Exception("Category not found");
        }
        return _mapper.Map<CategoryResponse>(category);
    }

    public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest categoryRequest, string name)
    {
        var category =  _mapper.Map<Category>(categoryRequest);
        if(category is BaseModel baseModel)
        {
            baseModel.CreatedAt = DateTime.Now;
            baseModel.CreatedBy = name;
        }
        await  _categoryRepository.AddAsync(category);

        return _mapper.Map<CategoryResponse>(category);
    }

    public async Task<CategoryResponse> UpdateCategoryAsync(Guid id, CategoryRequest categoryRequest, string name)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        switch (category)
        {
            case BaseModel baseModel:
                baseModel.ModifyAt = DateTime.Now;
                baseModel.ModifyBy = name;
                break;
            case null:
                throw new Exception("Book not found");
        }

        _mapper.Map(categoryRequest, category);
        await  _categoryRepository.UpdateAsync(category);

        return _mapper.Map<CategoryResponse>(category);
    }

    public async Task DeleteCategoryAsync(Guid id, string name)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            throw new Exception("Category not found");
        }

        category.ModifyAt = DateTime.Now;
        category.ModifyBy = name;
        category.IsDeleted = true;
        await _categoryRepository.UpdateAsync(category);
    }
}
