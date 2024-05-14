namespace EFCore.Repositories.DTOs;

public class OrganizationDto
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public Guid DepartmentId { get; set; }
    
    public DateOnly JoinedDate { get; set; }
    
}