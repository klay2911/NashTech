using System.Data;
using MVC.BusinessLogic.Services;
using MVC.Models.Models;
using MVC.Repositories.Repositories;

namespace MVC.BusinessLogic.Services;
public class PeopleService : IPeopleService
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleService(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            var members = await _peopleRepository.GetAllAsync();
            if (members == null)
            {
                throw new Exception("No members found");
            }
            return members;
        }

        public async Task<Member> GetByIdAsync(Guid id)
        {
            var member = await _peopleRepository.GetByIdAsync(id);
            if (member == null)
            {
                throw new Exception($"No member found with ID {id}");
            }
            return member;
        }

        public async Task<IEnumerable<Member>> GetMaleMembersAsync()
        {
            var members = await _peopleRepository.GetMaleMembersAsync();
            if (members == null)
            {
                throw new Exception("No male members found");
            }
            return members;
        }

        public async Task<Member> GetOldestPersonAsync()
        {
            var member = await _peopleRepository.GetOldestPersonAsync();
            if (member == null)
            {
                throw new Exception("No member found");
            }
            return member;
        }

        public async Task<IEnumerable<Member>> GetFullNameMembersAsync()
        {
            var members = await _peopleRepository.GetFullNameMembersAsync();
            if (members == null)
            {
                throw new Exception("No members found");
            }
            return members;
        }

        public async Task<IEnumerable<Member>> GetMembersBornInYearAsync(int year)
        {
            var members = await _peopleRepository.GetMembersBornInYearAsync(year);
            if (members == null)
            {
                throw new Exception($"No members were born in {year}");
            }
            return members;
        }

        public async Task<IEnumerable<Member>> GetMembersBornAfterYearAsync(int year)
        {
            var members = await _peopleRepository.GetMembersBornAfterYearAsync(year);
            if (members == null)
            {
                throw new Exception($"No members were born after {year}");
            }
            return members;
        }

        public async Task<IEnumerable<Member>> GetMembersBornBeforeYearAsync(int year)
        {
            var members = await _peopleRepository.GetMembersBornBeforeYearAsync(year);
            if (members == null)
            {
                throw new Exception($"No members were born before {year}");
            }
            return members;
        }

        public async Task<DataTable> ExportToExcelAsync()
        {
            var dataTable = await _peopleRepository.GetPeopleAsDataTableAsync();
            if (dataTable == null)
            {
                throw new Exception("No data found to export");
            }
            return dataTable;
        }

        public async Task AddAsync(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            await _peopleRepository.AddAsync(member);
        }

        public async Task UpdateAsync(Guid id, Member updatedMember)
        {
            if (updatedMember == null)
            {
                throw new ArgumentNullException(nameof(updatedMember));
            }
            var member = await _peopleRepository.GetByIdAsync(id);
            if (member == null)
            {
                throw new Exception($"No member found with ID {id}");
            }
            await _peopleRepository.UpdateAsync(id, updatedMember);
        }
        
        public async Task<bool> RemoveAsync(Guid id)
        {
            var member = await _peopleRepository.GetByIdAsync(id);
            if (member == null)
            {
                return false;
            }
            await _peopleRepository.RemoveAsync(id);
            return true;
        }
    }