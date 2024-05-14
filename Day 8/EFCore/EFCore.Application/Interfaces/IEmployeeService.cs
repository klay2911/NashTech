using EFCore.Repositories.DTOs;

namespace EFCore.Repositories.Interfaces;

public interface IEmployeeService 
{
    Task<IEnumerable<EmployeeDto>> GetAllAsync();
    
    Task<EmployeeDto> GetByIdAsync(Guid id);
    
    Task<EmployeeDto> AddAsync(EmployeeCreateDto objModel);
    
    Task<EmployeeDto> UpdateAsync(Guid id,EmployeeCreateDto objModel);
    
    Task<bool> DeleteAsync(Guid id);
}