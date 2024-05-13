using EFCore.Repositories.DTOs;
using EFCore.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.WebAPI.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : BaseController
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var getAll = await _employeeService.GetAllAsync();
            return Ok(getAll);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        try
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            return Ok(employee);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }

    
    /// <summary>
    /// to add new employee
    /// </summary>
    /// <param name="employee"> </param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] EmployeeCreateDto employee)
    {
        try
        {
            var add = await _employeeService.AddAsync(employee);
            return Ok(add);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromForm] EmployeeCreateDto employee)
    {
        try
        {
            var updatedEmployee = await _employeeService.UpdateAsync(id, employee);
            if (updatedEmployee == null)
            {
                return NotFound("Employee not found");
            }

            return Ok(updatedEmployee);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            var result = await _employeeService.DeleteAsync(id);
            if (!result)
            {
                return NotFound("Employee not found");
            }

            return Ok(new { message = "Employee deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }
}