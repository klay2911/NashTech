using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models.Models;

[Table("Salaries")]
public class Salary
{
    public Guid Id { get; set; }
    
    public Guid EmployeeId { get; set; }
    
    public decimal Wage { get; set; }
    
    public Employee? Employee { get; set;}
}