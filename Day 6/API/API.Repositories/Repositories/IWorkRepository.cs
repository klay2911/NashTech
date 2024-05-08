using API.Models;
using API.Repositories.Common;

namespace API.Repositories.Repositories;

public interface IWorkRepository
{ 
    Task<Work> GetWorkById(Guid id);
    
    Task<IEnumerable<Work>> GetAllWorks();
    
    Task AddWork(Work work);

    Task UpdateWork(Guid id, Work updateWork);

    Task DeleteWorkAsync(Guid id);
    
    Task AddBulkAsync(List<Work> works);

    Task DeleteBulkAsync(List<Guid> guids);
}