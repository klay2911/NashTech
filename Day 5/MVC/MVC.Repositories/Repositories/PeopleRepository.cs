using System.Collections;
using System.Data;
using MVC.Models.Enum;
using MVC.Models.Models;

namespace MVC.Repositories.Repositories;

public class PeopleRepository : IPeopleRepository
{
    static readonly List<Member> _people = new()
    {
        new Member("Nguyen Van", "Nam", GenderType.Male, new DateOnly(1999, 06, 02), "0945628812", "Nam Dinh",true),
        new Member("Do Tuan", "Duc", GenderType.Female, new DateOnly(2000, 11, 08), "0938428762", "Ha Noi",true),
        new Member("Hoang Thanh", "Huong", GenderType.Female, new DateOnly(2002, 4, 20), "0948348712", "VietNam", false)
    };

    public async Task<IEnumerable<Member>> GetAllAsync()
    {
        if (_people.Count == 0)
        {
            throw new Exception("No member found");
        }
        return await Task.FromResult(_people);
    }

    public async Task<IEnumerable<Member>> GetMaleMembersAsync()
    {
        var maleMembers = _people.Where(person => person.Gender == GenderType.Male).ToList();
        if (maleMembers.Count == 0)
        {
            throw new Exception("No male members found");
        }

        return await Task.FromResult(maleMembers);
    }

    public async Task<Member> GetOldestPersonAsync()
    {
        var oldestPerson = _people.MinBy(person => person.Dob);
        if (oldestPerson == null)
        {
            throw new Exception("No member found");
        }

        return await Task.FromResult(oldestPerson);
    }

    public async Task<IEnumerable<Member>> GetFullNameMembersAsync()
    {
        if (_people.Count == 0)
        {
            throw new Exception("No member found");
        }
        return await Task.FromResult(_people);
    }

    public async Task<Member> GetByIdAsync(Guid id)
    {
        var person = _people.FirstOrDefault(p => p.Id == id);
        if (person == null)
        {
            throw new Exception($"No member found with ID {id}");
        }
        return await Task.FromResult(person);
    }

    public async Task<IEnumerable<Member>> GetMembersBornInYearAsync(int year)
    {
        if (year < 0)
        {
            throw new ArgumentException("Year cannot be negative.", nameof(year));
        }

        var membersInYear = _people.Where(member => member.Dob.Year == year).ToList();

        if (membersInYear.Count == 0)
        {
            throw new Exception($"No members were born in {year}.");
        }

        return await Task.FromResult(membersInYear);
    }

    public async Task<IEnumerable<Member>> GetMembersBornAfterYearAsync(int year)
    {
        if (year < 0)
        {
            throw new ArgumentException("Year cannot be negative.", nameof(year));
        }

        var membersAfterYear = _people.Where(member => member.Dob.Year > year).ToList();

        if (membersAfterYear.Count == 0)
        {
            throw new Exception($"No members were born in {year}.");
        }

        return await Task.FromResult(membersAfterYear);
    }

    public async Task<IEnumerable<Member>> GetMembersBornBeforeYearAsync(int year)
    {
        if (year < 0)
        {
            throw new ArgumentException("Year cannot be negative.", nameof(year));
        }

        var membersBeforeYear = _people.Where(member => member.Dob.Year < year).ToList();

        if (membersBeforeYear.Count == 0)
        {
            throw new Exception($"No members were born in {year}.");
        }

        return await Task.FromResult(membersBeforeYear);
    }

    public async Task AddAsync(Member member)
    {
        _people.Add(member);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Guid id, Member updatedMember)
    {
        var person = await GetByIdAsync(id);
        person.FirstName = updatedMember.FirstName;
        person.LastName = updatedMember.LastName;
        person.Gender = updatedMember.Gender;
        person.Dob = updatedMember.Dob;
        person.PhoneNumber = updatedMember.PhoneNumber;
        person.BirthPlace = updatedMember.BirthPlace;
        await Task.CompletedTask;
    }

    public async Task RemoveAsync(Guid id)
    {
        var person = await GetByIdAsync(id);
        if (person == null)
        {
            throw new Exception($"No member found with ID {id}");
        }
        _people.Remove(person);
        await Task.CompletedTask;
    }

    public async Task<DataTable> GetPeopleAsDataTableAsync()
    {
        DataTable dt = new DataTable("Grid");
        var properties = typeof(Member).GetProperties();

        foreach (var prop in properties)
        {
            dt.Columns.Add(new DataColumn(prop.Name));
        }

        foreach (var person in _people)
        {
            var row = dt.NewRow();
            foreach (var prop in properties)
            {
                row[prop.Name] = prop.GetValue(person);
            }

            dt.Rows.Add(row);
        }

        return await Task.FromResult(dt);
    }
}