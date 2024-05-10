namespace EFCore.Models.Models;

public class Salary
{
    public Guid Id { get; set; }
    
    public Guid EmployeeId { get; set; }
    
    public decimal Wage { get; set; }
    
    public Employee? Employee { get; set;}
}