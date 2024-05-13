using EFCore.Models.Models;

namespace EFCore.Repositories.DTOs;

public class SalaryCreateDto
{
    public Guid EmployeeId { get; set; }
    
    public decimal Wage { get; set; }
    
    public SalaryCreateDto()
    {
    }
    
    public SalaryCreateDto(Salary salary)
    {
        EmployeeId = salary.EmployeeId;
        Wage = salary.Wage;
    }
}