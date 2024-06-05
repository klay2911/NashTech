using LibraryManagement.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository <TEntity> where TEntity : class
{
    protected readonly LibraryContext Context;

    protected BaseRepository(LibraryContext context)
    {
        Context = context;
    }
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id)
    {
        return (await Context.Set<TEntity>().FindAsync(id))!;
    }
    public virtual async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(TEntity objModel)
    {
        Context.Entry(objModel).State = EntityState.Modified;
        await Context.SaveChangesAsync();
        // using var transaction = _context.Database.BeginTransaction();
        // try
        // {
        //     _context.Entry(objModel).State = EntityState.Modified;
        //     _context.SaveChanges();
        //     transaction.Commit();
        // }
        // catch (Exception e)
        // {
        //     transaction.Rollback();
        //     throw new Exception(e.Message);
        // }
    }

    public virtual async Task DeleteAsync(TEntity objModel)
    {
        Context.Set<TEntity>().Remove(objModel);
        await Context.SaveChangesAsync();
    }
    
    public virtual async Task SaveAsync()
    {
        await Context.SaveChangesAsync();
    }
}