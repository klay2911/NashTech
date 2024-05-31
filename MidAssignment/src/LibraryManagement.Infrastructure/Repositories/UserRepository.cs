using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly LibraryContext _context;
    public UserRepository(LibraryContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> FindUserByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return null;
        }
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        return user;
    } 
}