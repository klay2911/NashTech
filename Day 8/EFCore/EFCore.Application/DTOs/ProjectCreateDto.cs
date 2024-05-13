using EFCore.Models.Models;

namespace EFCore.Repositories.DTOs;

public class ProjectCreateDto
{
    public string? Name { get; set; }
    
    public ProjectCreateDto()
    {
        
    }
    
    public ProjectCreateDto(Project project)
    {
        Name = project.Name;
    }
}