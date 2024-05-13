using EFCore.Models.Models;
using EFCore.Repositories.Interfaces;

namespace EFCore.Services.Repositories;

public class SalaryRepository : BaseRepository<Salary>, ISalaryRepository
{
    public SalaryRepository(CompanyContext context) : base(context)
    {
    }
}