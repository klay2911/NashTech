using EFCore.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.WebAPI.Controllers;

[ApiController]
[Route("organization")]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    /// <summary>
    /// Get all employees with their departments
    /// </summary>
    /// <returns> </returns>
    [HttpGet("employees-departments")]
    public async Task<IActionResult> GetEmployeesWithDepartments()
    {
        var result = await _organizationService.GetEmployeesWithDepartmentsAsync();
        return Ok(result);
    }

    /// <summary>
    /// Get all employees with their projects
    /// </summary>
    /// <returns></returns>
    [HttpGet("employees-projects")]
    public async Task<IActionResult> GetEmployeesWithProjects()
    {
        var result = await _organizationService.GetEmployeesWithProjectsAsync();
        return Ok(result);
    }
    
    [HttpGet("employees-high-salary-recent-join-date")]
    public async Task<IActionResult> GetEmployeesHighSalaryAndRecentJoinDate()
    {
        var result = await _organizationService.GetEmployeesHighSalaryAndRecentJoinDateAsync();
        return Ok(result);
    }
}