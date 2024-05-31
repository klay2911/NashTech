using LibraryManagement.Application.Base;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Application.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> FindUserByEmailAsync(string email);
}