using EFCore.Models.Models;
using EFCore.Repositories.Base;
using EFCore.Repositories.DTOs;

namespace EFCore.Repositories.Interfaces;

public interface IProjectService 
{
    Task<IEnumerable<ProjectDto>> GetAllAsync();
    
    Task<ProjectDto> GetByIdAsync(Guid id);

    Task<ProjectCreateDto> AddAsync(ProjectCreateDto objModel);

    Task<ProjectCreateDto> UpdateAsync(Guid id, ProjectCreateDto objModel);    
    Task<bool> DeleteAsync(Guid id);
}