using EFCore.Models.Models;
using EFCore.Repositories.Base;
using EFCore.Repositories.DTOs;

namespace EFCore.Repositories.Interfaces;

public interface ISalaryService 
{
    Task<IEnumerable<SalaryDto>> GetAllAsync();
    
    Task<SalaryDto> GetByIdAsync(Guid id);

    Task<SalaryCreateDto> AddAsync(SalaryCreateDto objModel);

    Task<SalaryCreateDto> UpdateAsync(Guid id, SalaryCreateDto objModel);    
    Task<bool> DeleteAsync(Guid id);
}