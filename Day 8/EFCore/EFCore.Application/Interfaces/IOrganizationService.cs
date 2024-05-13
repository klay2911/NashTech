using EFCore.Models.Models;
using EFCore.Repositories.DTOs;

namespace EFCore.Repositories.Interfaces;

public interface IOrganizationService
{
    Task<IEnumerable<EmployeeDepartmentDto>> GetEmployeesWithDepartmentsAsync();
    
    Task<IEnumerable<EmployeeProjectsDto>> GetEmployeesWithProjectsAsync();
    
    Task<IEnumerable<Employee>> GetEmployeesHighSalaryAndRecentJoinDateAsync();

}