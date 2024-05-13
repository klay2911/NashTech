using EFCore.Models.Models;
using EFCore.Repositories.Base;

namespace EFCore.Repositories.Interfaces;

public interface IEmployeeRepository : IBaseRepository<Employee>
{
}