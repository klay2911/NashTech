using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models.Models;

[Table("Project_Employee")]
public class ProjectEmployee
{
    public Guid ProjectId { get; set; }

    public Guid EmployeeId { get; set; }

    public bool Enable { get; set; }

    public Project Project { get; set; } = null!;

    public Employee Employee { get; set; } = null!;
}