using EFCore.Models.Models;

namespace EFCore.Repositories.DTOs;

public class EmployeeDepartmentDto
{
    public Employee? Employee { get; set; }
    public string DepartmentName { get; set; }
    

}