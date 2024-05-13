using EFCore.Models.Models;
using EFCore.Repositories.DTOs;
using EFCore.Repositories.Interfaces;

namespace EFCore.Services.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        var employees = (await _employeeRepository.GetAllAsync()).Select(employee => new EmployeeDto(employee));
        return employees;
    }

    public async Task<EmployeeDto> GetByIdAsync(Guid id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        var employeeDto = new EmployeeDto(employee);
        return employeeDto;
    }

    public async Task<EmployeeCreateDto> AddAsync(EmployeeCreateDto objModel)
    {
        var employee = new Employee
        {
            Name = objModel.Name!,
            DepartmentId = objModel.DepartmentId,
            JoinedDate = objModel.JoinedDate
        };
        await Task.Run(() => _employeeRepository.AddAsync(employee));
        return objModel;
    }

    public async Task<EmployeeCreateDto> UpdateAsync(Guid id, EmployeeCreateDto objModel)
    {
        var employee = new Employee
        {
            Id = id,
            Name = objModel.Name!,
            DepartmentId = objModel.DepartmentId,
            JoinedDate = objModel.JoinedDate
        };
        await Task.Run(() => _employeeRepository.UpdateAsync(employee));
        return objModel;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var employee = new Employee
        {
            Id = id
        };
        await Task.Run(() => _employeeRepository.DeleteAsync(employee));
        return true;
    }
}