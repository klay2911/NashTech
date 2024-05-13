using EFCore.Models.Models;
using EFCore.Repositories.DTOs;
using EFCore.Repositories.Interfaces;

namespace EFCore.Services.Services;

public class ProjectEmployeeService : IProjectEmployeeService
{
    private readonly IProjectEmployeeRepository _projectEmployeeRepository;

    public ProjectEmployeeService(IProjectEmployeeRepository projectEmployeeRepository)
    {
        _projectEmployeeRepository = projectEmployeeRepository;
    }
    
    public async Task<IEnumerable<ProjectEmployeeDto>> GetAllAsync()
    {
        var projectEmployees = (await _projectEmployeeRepository.GetAllAsync()).Select(projectEmployee => new ProjectEmployeeDto(projectEmployee));
        return projectEmployees;
    }

    public async Task<ProjectEmployeeDto> GetByIdAsync(Guid id)
    {
        var projectEmployee = await _projectEmployeeRepository.GetByIdAsync(id);
        var projectEmployeeDto = new ProjectEmployeeDto(projectEmployee);
        return projectEmployeeDto;
    }
    
    public async Task<ProjectEmployeeCreateDto> AddAsync(ProjectEmployeeCreateDto objModel)
    {
        var projectEmployee = new ProjectEmployee
        {
            EmployeeId = objModel.EmployeeId,
            Enable = objModel.Enable
        };
        await Task.Run(() => _projectEmployeeRepository.AddAsync(projectEmployee));
        return objModel;
    }

    public async Task<ProjectEmployeeCreateDto> UpdateAsync(Guid id, ProjectEmployeeCreateDto objModel)
    {
        var projectEmployee = new ProjectEmployee
        {
            ProjectId = id,
            EmployeeId = objModel.EmployeeId,
            Enable = objModel.Enable
        };
        await Task.Run(() => _projectEmployeeRepository.UpdateAsync(projectEmployee));
        return objModel;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var projectEmployee = new ProjectEmployee
        {
            ProjectId = id
        };
        await Task.Run(() => _projectEmployeeRepository.DeleteAsync(projectEmployee));
        return true;
    }
}