using EFCore.Models.Models;

namespace EFCore.Repositories.DTOs;

public class EmployeeProjectsDto
{
    public Employee? Employee { get; set; }
    public IEnumerable<Project> Projects { get; set; }

}