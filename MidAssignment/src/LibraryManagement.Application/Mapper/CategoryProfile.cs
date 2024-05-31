using AutoMapper;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Application.Mapper;

public class CategoryProfile : BaseProfile<Category, CategoryResponse, CategoryRequest>
{
}