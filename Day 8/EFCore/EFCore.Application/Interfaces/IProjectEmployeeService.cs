using EFCore.Models.Models;
using EFCore.Repositories.Base;
using EFCore.Repositories.DTOs;

namespace EFCore.Repositories.Interfaces;

public interface IProjectEmployeeService
{
    Task<IEnumerable<ProjectEmployeeDto>> GetAllAsync();
    
    Task<ProjectEmployeeDto> GetByIdAsync(Guid id);
    
    Task<ProjectEmployeeCreateDto> AddAsync(ProjectEmployeeCreateDto objModel);
    
    Task<ProjectEmployeeCreateDto> UpdateAsync(Guid id,ProjectEmployeeCreateDto objModel);
    
    Task<bool> DeleteAsync(Guid id);
}