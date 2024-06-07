using AutoMapper;
using LibraryManagement.Application.DTOs.CategoryDTOs;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Application.Mapper;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryResponse>();
        CreateMap<CategoryRequest, Category>();
    }
}