using API.Models;

namespace API.Repositories.Repositories;

public class WorkRepository : IWorkRepository
{
    private static readonly List<Work> Works = new()
    {
        new Work("Work 1", true),
        new Work("Work 2", false),
        new Work("Work 3", false),
        new Work("Work 4", true),
        new Work("Work 5", false),
        new Work("Work 6", false),
    };
    public async Task<Work> GetWorkById(Guid id)
    {
        var work = Works.FirstOrDefault(p => p.Id == id);
        if (work == null)
        {
            throw new Exception($"No member found with ID {id}");
        }
        return await Task.FromResult(work);
    }

    public async Task<IEnumerable<Work>> GetAllWorks()
    {
        if (Works.Count == 0)
        {
            throw new Exception("No member found");
        }
        return await Task.FromResult(Works);
    }

    public async Task AddWork(Work work)
    {
        Works.Add(work);
        await Task.CompletedTask;
    }

    public async Task UpdateWork (Guid id, Work updateWork)
    {
        var work = GetWorkById(id);
        if (work == null)
        {
            throw new Exception($"No member found with ID {id}");
        }
        work.Result.Title = updateWork.Title;
        work.Result.IsCompleted = updateWork.IsCompleted;
        await Task.CompletedTask;
    }

    public async Task DeleteWorkAsync(Guid id)
    {
        var work = await GetWorkById(id);
        if (work == null)
        {
            throw new Exception($"No member found with ID {id}");
        }
        Works.Remove(work);
        await Task.CompletedTask;
    }

    public async Task AddBulkAsync(List<Work> works)
    {
        Works.AddRange(works);
        await Task.CompletedTask;
    }

    public async Task DeleteBulkAsync(List<Guid> guids)
    {
        var worksToDelete = Works.Where(work => guids.Contains(work.Id)).ToList();
        Works.RemoveAll(work => worksToDelete.Contains(work));
        await Task.CompletedTask;
    }
}