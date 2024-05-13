using EFCore.Models.Models;
using EFCore.Repositories.Interfaces;

namespace EFCore.Services.Repositories;

public class ProjectEmployeeRepository : BaseRepository<ProjectEmployee>
{
    public ProjectEmployeeRepository(CompanyContext context) : base(context)
    {
    }
}