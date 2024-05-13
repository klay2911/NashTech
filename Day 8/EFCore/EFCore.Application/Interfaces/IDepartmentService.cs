using EFCore.Repositories.DTOs;

namespace EFCore.Repositories.Interfaces;

public interface IDepartmentService 
{
    Task<IEnumerable<DepartmentDto>> GetAllAsync();
    
    Task<DepartmentDto> GetByIdAsync(Guid id);
    
    Task<DepartmentCreateDto> AddAsync(DepartmentCreateDto objModel);
    
    Task<DepartmentCreateDto> UpdateAsync(Guid id,DepartmentCreateDto objModel);
    
    Task<bool> DeleteAsync(Guid id);
}