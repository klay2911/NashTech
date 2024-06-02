using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(LibraryContext context) : base(context)
    {
    }
}