using EFCore.Models.Models;

namespace EFCore.Repositories.DTOs;

public class ProjectEmployeeCreateDto
{
    public Guid EmployeeId { get; set; }

    public bool Enable { get; set; }
    
    public ProjectEmployeeCreateDto()
    {
    }
    
    public ProjectEmployeeCreateDto(ProjectEmployee projectEmployee)
    {
        EmployeeId = projectEmployee.EmployeeId;
        Enable = projectEmployee.Enable;
    }
}