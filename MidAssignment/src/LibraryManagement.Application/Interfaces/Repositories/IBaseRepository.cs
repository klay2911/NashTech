namespace LibraryManagement.Application.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    
    Task<T> GetByIdAsync(Guid id);
    
    Task AddAsync(T entity);
    
    Task UpdateAsync(T objModel);
    
    Task DeleteAsync(T objModel);
    
    Task SaveAsync();
}