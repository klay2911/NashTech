using System.Text.Json.Serialization;
using API.Models.Enum;
using API.Models.Models;

namespace API.Repositories.Repositories;

public class MemberRepository : IMemberRepository
{
    static readonly List<Member> Members = new()
    {
        new Member("Nguyen Van", "Nam", GenderType.Male, new DateOnly(1999, 06, 02), "0945628812", "Nam Dinh",true),
        new Member("Do Tuan", "Duc", GenderType.Female, new DateOnly(2000, 11, 08), "0938428762", "Ha Noi",true),
        new Member("Hoang Thanh", "Huong", GenderType.Female, new DateOnly(2002, 4, 20), "0948348712", "VietNam", false),
        new Member("Hoang Thanh", "a", GenderType.Female, new DateOnly(2002, 4, 10), "0948348712", "VietNam", false),
        new Member("Hoang Thanh", "b", GenderType.Female, new DateOnly(2002, 4, 20), "0948342312", "VietNam", false),
        new Member("Hoang Thanh", "c", GenderType.Male, new DateOnly(2002, 4, 5), "0948354612", "VietNam", false),
        new Member("Hoang Thanh", "d", GenderType.Female, new DateOnly(2002, 4, 3), "0975348712", "VietNam", false),
        new Member("Hoang Thanh", "e", GenderType.Male, new DateOnly(2002, 4, 2), "0948354712", "VietNam", false)
    };
    
    public async Task<IEnumerable<Member>> GetAllMembersAsync()
    {
        if (Members.Count == 0)
        {
            throw new Exception("No member found");
        }
        return await Task.FromResult(Members);
    }

    public async Task<Member> GetMemberByIdAsync(Guid id)
    {
        var member = Members.FirstOrDefault(p => p.Id == id);
        if (member == null)
        {
            throw new Exception($"No member found with ID {id}");
        }
        return await Task.FromResult(member);
    }

    public async Task AddMemberAsync(Member member)
    {
        Members.Add(member);
        await Task.CompletedTask;
    }

    public async Task UpdateMemberAsync(Guid id, Member updateMember)
    {
        var member = GetMemberByIdAsync(id);
        if (member == null)
        {
            throw new Exception($"No member found with ID {id}");
        }
        member.Result.FirstName = updateMember.FirstName;
        member.Result.LastName = updateMember.LastName;
        member.Result.Gender = updateMember.Gender;
        member.Result.Dob = updateMember.Dob;
        member.Result.PhoneNumber = updateMember.PhoneNumber;
        member.Result.BirthPlace = updateMember.BirthPlace;
        await Task.CompletedTask;
    }

    public async Task DeleteMemberAsync(Guid id)
    {
        var member = await GetMemberByIdAsync(id);
        if (member == null)
        {
            throw new Exception($"No member found with ID {id}");
        }
        Members.Remove(member);
        await Task.CompletedTask;
    }
    
    private static class FilterNames
    {
        [JsonPropertyName("Name")]
        public const string Name = "Name";
        /// <summary>
        /// GenderType is an enum with 4 values {1: Unknown, 2: Male, 3: Female, 4: Other }
        /// </summary>
        [JsonPropertyName("Gender")]
        public const string Gender = "Gender";
        [JsonPropertyName("BirhtPlace")]
        public const string BirthPlace = "BirthPlace";
    }
    public async Task<IEnumerable<Member>> GetByFilter(Dictionary<string, string> filters)
    {
        var query = Members.AsQueryable();
        
        if (filters.ContainsKey(FilterNames.Name))
        {
            query = query.Where(p => (p.FirstName.Contains(filters[FilterNames.Name])) || (p.LastName.Contains(filters[FilterNames.Name])));
        }
        
        if (filters.TryGetValue(FilterNames.Gender, out var filter))
        {
            if (Enum.TryParse(typeof(GenderType), filter, true, out var genderValue))
            {
                query = query.Where(p => p.Gender == (GenderType)genderValue);
            }
        }
        
        if (filters.TryGetValue(FilterNames.BirthPlace, out var filter1))
        {
            query = query.Where(p => p.BirthPlace == filter1);
        }
        
        return await Task.FromResult(query.ToList());
    }
}
