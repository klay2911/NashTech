using EFCore.Models.Models;
using EFCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Services.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(CompanyContext context) : base(context)
    {
    }
}