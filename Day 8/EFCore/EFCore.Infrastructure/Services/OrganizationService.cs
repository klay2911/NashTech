using EFCore.Models.Models;
using EFCore.Repositories.DTOs;
using EFCore.Repositories.Interfaces;

namespace EFCore.Services.Services;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository _organizationRepository;

    public OrganizationService(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<IEnumerable<EmployeeDepartmentDto>> GetEmployeesWithDepartmentsAsync()
    {
        var result = await _organizationRepository.GetEmployeesWithDepartmentsAsync();

        return result.Select(r => new EmployeeDepartmentDto
        {
            Employee = r.employee,
            DepartmentName = r.departmentName
        });
    }

    public async Task<IEnumerable<EmployeeProjectsDto>> GetEmployeesWithProjectsAsync()
    {
        var result = await _organizationRepository.GetEmployeesWithProjectsAsync();

        return result.Select(r => new EmployeeProjectsDto
        {
            Employee = r.employee,
            Projects = r.projects
        });
    }
    public async Task<IEnumerable<Employee>> GetEmployeesHighSalaryAndRecentJoinDateAsync()
    {
        return await _organizationRepository.GetEmployeesHighSalaryAndRecentJoinDateAsync();
    }
}