namespace LibraryManagement.Application.Base;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    
    Task<T> GetByIdAsync(Guid id);
    
    void AddAsync(T entity);
    
    void UpdateAsync(T objModel);
    
    void DeleteAsync(T objModel);
    
}