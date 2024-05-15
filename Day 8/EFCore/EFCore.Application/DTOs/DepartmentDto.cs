using System.Text.Json.Serialization;
using EFCore.Models.Common;
using EFCore.Models.Models;

namespace EFCore.Repositories.DTOs;

//[JsonConverter(typeof(NoReferenceHandlingConverter<DepartmentDto>))]
public class DepartmentDto
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }

    public DepartmentDto(Department department)
    {
        Id = department.Id;
        Name = department.Name;
    }
}