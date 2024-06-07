using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(LibraryContext context) : base(context)
    {
    }
    public override async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await Context.Set<Category>().Where(category => !category.IsDeleted).ToListAsync();
    }
}