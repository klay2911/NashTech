using API.Models;

namespace API.Service.Services;

public interface IWorkService
{
    Task<IEnumerable<Work>> GetWorksAsync();
    
    Task<Work> GetWorkByIdAsync(Guid id);
    
    Task<Work> AddWorkAsync(Work work);
    
    Task<Work> UpdateWorkAsync(Guid id, Work work);
    Task DeleteWorkAsync(Guid id);
    
    Task AddBulkWorkAsync(List<Work> works);
    
    Task DeleteBulkWorkAsync(List<Guid> guids);

}