using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models.Models;

[Table("Projects")]
public class Project
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }

    public ICollection<ProjectEmployee> ProjectEmployees { get; } = new List<ProjectEmployee>();
} 