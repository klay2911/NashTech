using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models.Models;

[Table("Departments")]
public class Department
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
    
    public ICollection<Employee> Employees { get; } = new List<Employee>();
}