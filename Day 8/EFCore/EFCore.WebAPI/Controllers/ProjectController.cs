using EFCore.Repositories.DTOs;
using EFCore.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : BaseController
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await _projectService.GetAllAsync();
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var project = await _projectService.GetByIdAsync(id);
        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm]ProjectCreateDto project)
    {
        var createdProject = await _projectService.AddAsync(project);
        return Ok(createdProject);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromForm]ProjectCreateDto project)
    {
        var updatedProject = await _projectService.UpdateAsync(id, project);
        return Ok(updatedProject);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _projectService.DeleteAsync(id);
        return Ok(result);
    }
}