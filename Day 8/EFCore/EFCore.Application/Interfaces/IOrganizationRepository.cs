using EFCore.Models.Models;

namespace EFCore.Repositories.Interfaces;

public interface IOrganizationRepository
{
    Task<IEnumerable<(Employee employee, string departmentName)>> GetEmployeesWithDepartmentsAsync();
    
    Task<IEnumerable<(Employee employee, IEnumerable<Project> projects)>> GetEmployeesWithProjectsAsync();

    Task<IEnumerable<Employee>> GetEmployeesHighSalaryAndRecentJoinDateAsync();

}