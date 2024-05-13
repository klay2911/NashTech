using EFCore.Models.Models;

namespace EFCore.Repositories.DTOs;

public class ProjectEmployeeDto
{
    public Guid ProjectId { get; set; }

    public Guid EmployeeId { get; set; }

    public bool Enable { get; set; }
    
    public ProjectEmployeeDto(ProjectEmployee projectEmployee)
    {
        ProjectId = projectEmployee.ProjectId;
        EmployeeId = projectEmployee.EmployeeId;
        Enable = projectEmployee.Enable;
    }
}