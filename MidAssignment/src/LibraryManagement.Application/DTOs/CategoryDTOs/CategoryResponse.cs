using LibraryManagement.Domain.Enum;

namespace LibraryManagement.Application.DTOs.Responses;

public class CategoryResponse
{
    public Guid CategoryId { get; set; }
    
    public string? Name { get; set; }
}