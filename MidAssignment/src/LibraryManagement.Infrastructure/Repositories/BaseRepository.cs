using LibraryManagement.Application.Base;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository <TEntity> where TEntity : class
{
    private readonly LibraryContext _context;

    protected BaseRepository(LibraryContext context)
    {
        _context = context;
    }
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id)
    {
        return (await _context.Set<TEntity>().FindAsync(id))!;
    }

    public virtual void AddAsync(TEntity entity)
    {
        _context.Set<TEntity>().AddAsync(entity);
        _context.SaveChangesAsync();
        // using var transaction = _context.Database.BeginTransaction();
        // try
        // {
        //     _context.Set<TEntity>().Add(entity);
        //     _context.SaveChanges();
        //     transaction.Commit();
        // }
        // catch (Exception e)
        // {
        //     transaction.Rollback();
        //     throw new Exception(e.Message);
        // }
    }

    public virtual void UpdateAsync(TEntity objModel)
    {
        _context.Entry(objModel).State = EntityState.Modified;
        _context.SaveChangesAsync();
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

    public virtual void DeleteAsync(TEntity objModel)
    {
        _context.Set<TEntity>().Remove(objModel);
        _context.SaveChangesAsync();
    }
    
    // public virtual async Task SaveAsync()
    // {
    //     await _context.SaveChangesAsync();
    // }
}