namespace EFCore.Models.Models;

public class ProjectEmployee
{
    public Guid ProjectId { get; set; }
    
    public Guid EmployeeId { get; set; }
    
    public Boolean Enable { get; set; }
    
    public Project Project { get; set; } = null!;
    
    public Employee Employee { get; set; } = null!;
}