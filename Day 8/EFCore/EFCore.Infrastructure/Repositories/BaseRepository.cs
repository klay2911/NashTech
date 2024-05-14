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
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new Exception(e.Message);
            }
        }
    }

    public virtual void UpdateAsync(TEntity objModel)
    {
        using( var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                _context.Entry(objModel).State = EntityState.Modified;
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new Exception(e.Message);
            }
        }
    }

    public virtual void DeleteAsync(TEntity objModel)
    {
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                _context.Set<TEntity>().Remove(objModel);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new Exception(e.Message);
            }
        }
    }
}