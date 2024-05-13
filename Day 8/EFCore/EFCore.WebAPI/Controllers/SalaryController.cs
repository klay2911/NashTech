using EFCore.Repositories.DTOs;
using EFCore.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalaryController : BaseController
{
    private readonly ISalaryService _salaryService;

    public SalaryController(ISalaryService salaryService)
    {
        _salaryService = salaryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var salaries = await _salaryService.GetAllAsync();
        return Ok(salaries);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var salary = await _salaryService.GetByIdAsync(id);
        return Ok(salary);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm]SalaryCreateDto salary)
    {
        var createdSalary = await _salaryService.AddAsync(salary);
        return Ok(createdSalary);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromForm]SalaryCreateDto salary)
    {
        var updatedSalary = await _salaryService.UpdateAsync(id, salary);
        return Ok(updatedSalary);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _salaryService.DeleteAsync(id);
        return Ok(result);
    }
}