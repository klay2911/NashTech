using System.Data;
using MVC.Models.Models;

namespace MVC.Repositories.Repositories;

public interface IPeopleRepository
{
    Task<IEnumerable<Member>> GetAllAsync();

    Task<IEnumerable<Member>> GetMaleMembersAsync();

    Task<Member> GetOldestPersonAsync();

    Task<IEnumerable<Member>> GetFullNameMembersAsync();

    Task<Member> GetByIdAsync(Guid id);
    
    Task<IEnumerable<Member>> GetMembersBornInYearAsync(int year);

    Task<IEnumerable<Member>> GetMembersBornAfterYearAsync(int year);

    Task<IEnumerable<Member>> GetMembersBornBeforeYearAsync(int year);

    Task AddAsync(Member member);

    Task UpdateAsync(Guid id, Member updatedMember);

    Task RemoveAsync(Guid id);
    
    Task<DataTable> GetPeopleAsDataTableAsync();
}