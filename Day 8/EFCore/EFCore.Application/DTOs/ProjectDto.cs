using EFCore.Models.Models;

namespace EFCore.Repositories.DTOs;

public class ProjectDto
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public ProjectDto(Project project)
    {
        Id = project.Id;
        Name = project.Name;
    }
}