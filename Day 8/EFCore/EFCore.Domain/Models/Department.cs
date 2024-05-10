using System.ComponentModel.DataAnnotations;

namespace EFCore.Models.Models;

public class Department
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
    
    public ICollection<Employee> Employees { get; } = new List<Employee>();
}