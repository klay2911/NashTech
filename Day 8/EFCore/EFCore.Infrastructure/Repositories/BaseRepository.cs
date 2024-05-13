using EFCore.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Services.Repositories;

public class BaseRepository<TEntity> : IBaseRepository <TEntity> where TEntity : class
{
    private readonly CompanyContext _context;
    
    public BaseRepository(CompanyContext context)
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
        _context.SaveChanges();
    }

    public virtual void UpdateAsync(TEntity objModel)
    {
        _context.Entry(objModel).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public virtual void DeleteAsync(TEntity objModel)
    {
        _context.Set<TEntity>().Remove(objModel);
        _context.SaveChanges();
    }
}