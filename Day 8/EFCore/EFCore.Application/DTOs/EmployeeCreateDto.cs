using System.ComponentModel.DataAnnotations;
using EFCore.Models.Common;
using EFCore.Models.Models;

namespace EFCore.Repositories.DTOs;

public class EmployeeCreateDto
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public Guid DepartmentId { get; set; }
    [Required]
    [MaxLength(15)]
    [ValidDate]
    public string JoinedDate { get; set; }
    
    public EmployeeCreateDto()
    {
        
    }
    public EmployeeCreateDto(Employee employee)
    {
        Name = employee.Name;
        DepartmentId = employee.DepartmentId;
        JoinedDate = employee.JoinedDate.ToString("dd/MM/yyyy");
    }
}