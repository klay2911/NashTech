using EFCore.Repositories.DTOs;

namespace EFCore.Repositories.Interfaces;

public interface IEmployeeService 
{
    Task<IEnumerable<EmployeeDto>> GetAllAsync();
    
    Task<EmployeeDto> GetByIdAsync(Guid id);
    
    Task<EmployeeCreateDto> AddAsync(EmployeeCreateDto objModel);
    
    Task<EmployeeCreateDto> UpdateAsync(Guid id,EmployeeCreateDto objModel);
    
    Task<bool> DeleteAsync(Guid id);
}