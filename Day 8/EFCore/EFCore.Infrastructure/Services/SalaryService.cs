using EFCore.Models.Models;
using EFCore.Repositories.DTOs;
using EFCore.Repositories.Interfaces;

namespace EFCore.Services.Services;

public class SalaryService : ISalaryService
{
    private readonly ISalaryRepository _salaryRepository;

    public SalaryService(ISalaryRepository salaryRepository)
    {
        _salaryRepository = salaryRepository;
    }

    public async Task<IEnumerable<SalaryDto>> GetAllAsync()
    {
        var salaries = (await _salaryRepository.GetAllAsync()).Select(salary => new SalaryDto(salary));
        return salaries;
    }

    public async Task<SalaryDto> GetByIdAsync(Guid id)
    {
        var salary = await _salaryRepository.GetByIdAsync(id);
        var salaryDto = new SalaryDto(salary);
        return salaryDto;
    }

    public async Task<SalaryCreateDto> AddAsync(SalaryCreateDto objModel)
    {
        var salary = new Salary
        {
            EmployeeId = objModel.EmployeeId,
            Wage = objModel.Wage
        };
        await Task.Run(() => _salaryRepository.AddAsync(salary));
        return objModel;
    }

    public async Task<SalaryCreateDto> UpdateAsync(Guid id, SalaryCreateDto objModel)
    {
        var salary = new Salary
        {
            Id = id,
            EmployeeId = objModel.EmployeeId,
            Wage = objModel.Wage
        };
        await Task.Run(() => _salaryRepository.UpdateAsync(salary));
        return objModel;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var salary = new Salary
        {
            Id = id
        };
        await Task.Run(() => _salaryRepository.DeleteAsync(salary));
        return true;
    }
}