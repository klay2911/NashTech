using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models.Models;

[Table("Employees")]
public class Employee
{
    [Key]
    public Guid Id;
    
    [Required]
    [MaxLength(100)]
    [Column("EmployeeName", TypeName = "nvarchar(100)")]
    public string Name;
    
    public Guid DepartmentId;
    
    public DateOnly JoinedDate;
    
    public Salary? Salary { get; set; }
    
    public Department? Department { get; set; }
    
    // public ICollection<Project>? Projects { get; } = new List<Project>();
    public ICollection<ProjectEmployee> ProjectEmployees { get; } = new List<ProjectEmployee>();
}