using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(LibraryContext context) : base(context)
    {
    }
}