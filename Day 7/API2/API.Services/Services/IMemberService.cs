using API.Models.Models;

namespace API.Services.Services;

public interface IMemberService
{
    Task<IEnumerable<Member>> GetMembersAsync();
    
    Task<Member> GetMemberByIdAsync(Guid id);

    Task<Member> AddMemberAsync(Member member);
    
    Task<Member> UpdateMemberAsync(Guid id, Member member);
    
    Task DeleteMemberAsync(Guid id);

    Task<IEnumerable<Member>> GetByFilterAsync(Dictionary<string, string> filters);
}