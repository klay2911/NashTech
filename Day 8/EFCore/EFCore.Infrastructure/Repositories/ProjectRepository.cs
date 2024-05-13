using EFCore.Models.Models;
using EFCore.Repositories.Interfaces;

namespace EFCore.Services.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
    public ProjectRepository(CompanyContext context) : base(context)
    {
    }
}