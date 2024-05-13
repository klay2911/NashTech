using EFCore.Repositories.DTOs;
using EFCore.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.WebAPI.Controllers;

[ApiController]
[Route("api/departments")]
public class DepartmentController : BaseController
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var getAll = await _departmentService.GetAllAsync();
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
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound("Department not found");
            }

            return Ok(department);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] DepartmentCreateDto department)
    {
        try
        {
            var add = await _departmentService.AddAsync(department);
            return Ok(add);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromForm] DepartmentCreateDto department)
    {
        try
        {
            var updatedDepartment = await _departmentService.UpdateAsync(id, department);
            if (updatedDepartment == null)
            {
                return NotFound("Department not found");
            }

            return Ok(updatedDepartment);
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
            var result = await _departmentService.DeleteAsync(id);
            if (!result)
            {
                return NotFound("Department not found");
            }

            return Ok(new { message = "Department deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }
}