using EFCore.Models.Models;

namespace EFCore.Repositories.DTOs;

public class SalaryDto
{
    public Guid Id { get; set; }
    
    public Guid EmployeeId { get; set; }
    
    public decimal Wage { get; set; }
    
    public SalaryDto(Salary salary)
    {
        Id = salary.Id;
        EmployeeId = salary.EmployeeId;
        Wage = salary.Wage;
    }
}