using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.DTOs.CategoryDTOs;

public class CategoryRequest
{
    [Required]
    public string? Name { get; set; }
    
}