using EFCore.Models.Models;

namespace EFCore.Repositories.DTOs;

public class DepartmentCreateDto
{
    public string? Name { get; set; }

    public DepartmentCreateDto()
    {
        
    }

    public DepartmentCreateDto(Department department)
    {
        Name = department.Name;
    }
}