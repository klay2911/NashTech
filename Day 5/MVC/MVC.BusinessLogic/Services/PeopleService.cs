using System.Data;
using MVC.Models.Models;
using MVC.Repositories.Repositories;

namespace MVC.BusinessLogic.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleService(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _peopleRepository.GetAllAsync();
        }

        public async Task<Member> GetByIdAsync(Guid id)
        {
            return await _peopleRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Member>> GetMaleMembersAsync()
        {
            return await _peopleRepository.GetMaleMembersAsync();
        }

        public async Task<Member> GetOldestPersonAsync()
        {
            return await _peopleRepository.GetOldestPersonAsync();
        }
        
        public async Task<IEnumerable<Member>> GetFullNameMembersAsync()
        {
            return await _peopleRepository.GetFullNameMembersAsync();
        }

        public async Task<IEnumerable<Member>> GetMembersBornInYearAsync(int year)
        {
            return await _peopleRepository.GetMembersBornInYearAsync(year);
        }
        
        public async Task<IEnumerable<Member>> GetMembersBornAfterYearAsync(int year)
        {
            return await _peopleRepository.GetMembersBornAfterYearAsync(year);
        }
        
        public async Task<IEnumerable<Member>> GetMembersBornBeforeYearAsync(int year)
        {
            return await _peopleRepository.GetMembersBornBeforeYearAsync(year);
        }

        public async Task<DataTable> ExportToExcelAsync()
        { 
           return await _peopleRepository.GetPeopleAsDataTableAsync();
        }

        public async Task AddAsync(Member member)
        {
            await _peopleRepository.AddAsync(member);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _peopleRepository.RemoveAsync(id);
        }

        public async Task UpdateAsync(Guid id, Member updatedMember)
        {
            await _peopleRepository.UpdateAsync(id, updatedMember);
        }
    }
}
    // public (List<Person>, List<Person>, List<Person>) GetMembersByYear(int year)
    // {
    //     var bornInYear = _personRepository.GetMembersBornInYear(year);
    //     var bornAfterYear = _personRepository.GetMembersBornAfterYear(year);
    //     var bornBeforeYear = _personRepository.GetMembersBornBeforeYear(year);
    //
    //     return (bornInYear, bornAfterYear, bornBeforeYear);
    // }