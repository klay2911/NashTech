using EFCore.Models.Models;
using EFCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Services.Repositories;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly CompanyContext _context;
    public OrganizationRepository(CompanyContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<(Employee employee, string departmentName)>> GetEmployeesWithDepartmentsAsync()
    {
        var result = await (from e in _context.Set<Employee>()
            join d in _context.Set<Department>() on e.DepartmentId equals d.Id
            select new { Employee = e, DepartmentName = d.Name }).ToListAsync();

        return result.Select(r => (r.Employee, r.DepartmentName));
    }

    public async Task<IEnumerable<(Employee employee, IEnumerable<Project> projects)>> GetEmployeesWithProjectsAsync()
    {
        var result = await (from e in _context.Set<Employee>()
            join pe in _context.Set<ProjectEmployee>() on e.Id equals pe.EmployeeId into peGroup
            from pe in peGroup.DefaultIfEmpty()
            join p in _context.Set<Project>() on pe.ProjectId equals p.Id into pGroup
            select new { Employee = e, Projects = pGroup }).ToListAsync();

        return result.Select(r => (r.Employee, r.Projects));
    }
    public async Task<IEnumerable<Employee>> GetEmployeesHighSalaryAndRecentJoinDateAsync()
    {
        var employees = await _context.Employees
            .FromSqlRaw("SELECT E.* FROM Employee E INNER JOIN Salary S ON E.Id = S.EmployeeId AND S.Wage >= 100 AND E.JoinedDate >= '2024-01-01'")
            .ToListAsync();

        return employees;
    }
}