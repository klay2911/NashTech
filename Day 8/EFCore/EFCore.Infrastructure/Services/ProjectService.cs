using EFCore.Models.Models;
using EFCore.Repositories.DTOs;
using EFCore.Repositories.Interfaces;

namespace EFCore.Services.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<ProjectDto>> GetAllAsync()
    {
        var projects = (await _projectRepository.GetAllAsync()).Select(project => new ProjectDto(project));
        return projects;
    }

    public async Task<ProjectDto> GetByIdAsync(Guid id)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        var projectDto = new ProjectDto(project);
        return projectDto;
    }

    public async Task<ProjectCreateDto> AddAsync(ProjectCreateDto objModel)
    {
        var project = new Project
        {
            Name = objModel.Name!
        };
        await Task.Run(() => _projectRepository.AddAsync(project));
        return objModel;
    }

    public async Task<ProjectCreateDto> UpdateAsync(Guid id, ProjectCreateDto objModel)
    {
        var project = new Project
        {
            Id = id,
            Name = objModel.Name!
        };
        await Task.Run(() => _projectRepository.UpdateAsync(project));
        return objModel;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var project = new Project
        {
            Id = id
        };
        await Task.Run(() => _projectRepository.DeleteAsync(project));
        return true;
    }
}