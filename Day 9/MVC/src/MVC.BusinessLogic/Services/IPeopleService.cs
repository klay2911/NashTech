using System.Data;
using MVC.Models.Models;

namespace MVC.BusinessLogic.Services;

public interface IPeopleService
{
    Task<IEnumerable<Member>> GetAllAsync();

    Task<Member> GetByIdAsync(Guid id);

    Task<IEnumerable<Member>> GetMaleMembersAsync();
    
    Task<Member> GetOldestPersonAsync();

    Task<IEnumerable<Member>> GetFullNameMembersAsync();

    Task<IEnumerable<Member>> GetMembersBornInYearAsync(int year);

    Task<IEnumerable<Member>> GetMembersBornAfterYearAsync(int year);

    Task<IEnumerable<Member>> GetMembersBornBeforeYearAsync(int year);

    Task<DataTable> ExportToExcelAsync();

    Task AddAsync(Member Member);
    
    Task UpdateAsync(Guid id, Member updatedMember);

    Task<bool> RemoveAsync(Guid id);
    
}