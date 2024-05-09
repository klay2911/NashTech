using API.Models.Models;

namespace API.Repositories.Repositories;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllMembersAsync();
    
    Task<Member> GetMemberByIdAsync(Guid id);
    
    Task AddMemberAsync(Member member);
    
    Task UpdateMemberAsync(Guid id, Member updateMember);

    Task DeleteMemberAsync(Guid id);

    Task<IEnumerable<Member>> GetByFilter(Dictionary<string, string> filters);
}
