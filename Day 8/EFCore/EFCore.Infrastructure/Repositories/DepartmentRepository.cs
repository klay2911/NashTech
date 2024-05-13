using EFCore.Models.Models;
using EFCore.Repositories.Base;
using EFCore.Repositories.Interfaces;

namespace EFCore.Services.Repositories;

public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
{
    public DepartmentRepository(CompanyContext context) : base(context)
    {
    }
}