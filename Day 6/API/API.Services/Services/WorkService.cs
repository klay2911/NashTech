using API.Models;
using API.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Service.Services
{
    public class WorkService : IWorkService
    {
        private readonly IWorkRepository _workRepository;

        public WorkService(IWorkRepository workRepository)
        {
            _workRepository = workRepository;
        }

        public async Task<IEnumerable<Work>> GetWorksAsync()
        {
            return await _workRepository.GetAllWorks();
        }

        public async Task<Work> GetWorkByIdAsync(Guid id)
        {
            return await _workRepository.GetWorkById(id);
        }

        public async Task<Work> AddWorkAsync(Work work)
        {
            await _workRepository.AddWork(work);
            return work;
        }

        public async Task<Work> UpdateWorkAsync(Guid id, Work work)
        {
            await _workRepository.UpdateWork(id, work);
            return work;
        }
        public async Task DeleteWorkAsync(Guid id)
        {
            await _workRepository.DeleteWorkAsync(id);
        }

        public async Task AddBulkWorkAsync(List<Work> works)
        {
            await _workRepository.AddBulkAsync(works);
        }

        public async Task DeleteBulkWorkAsync(List<Guid> guids)
        {
            await _workRepository.DeleteBulkAsync(guids);
        }
    }
}
