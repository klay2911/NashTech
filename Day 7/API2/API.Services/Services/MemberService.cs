using API.Models.Models;
using API.Repositories.Repositories;

namespace API.Services.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<IEnumerable<Member>> GetMembersAsync()
    {
        return await _memberRepository.GetAllMembersAsync();
    }

    public async Task<Member> GetMemberByIdAsync(Guid id)
    {
        return await _memberRepository.GetMemberByIdAsync(id);
    }

    public async Task<Member> AddMemberAsync(Member member)
    {
        await _memberRepository.AddMemberAsync(member);
        return member;
    }

    public async Task<Member> UpdateMemberAsync(Guid id, Member member)
    {
        await _memberRepository.UpdateMemberAsync(id, member);
        return member;
    }

    public async Task DeleteMemberAsync(Guid id)
    {
        await _memberRepository.DeleteMemberAsync(id);
    }
    
    public async Task<IEnumerable<Member>> GetByFilterAsync(Dictionary<string, string> filters)
    {
        var members = await _memberRepository.GetByFilter(filters);
        return members ?? Enumerable.Empty<Member>();
    }
}