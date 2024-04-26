namespace CsFun2;

public class MemberManager
{
    // Male member
    public void PrintMaleMembers(List<Member> members)
    {
        var maleMembers = members.Where(member => member.Gender == "Nam").ToList();
        if (maleMembers.Count == 0)
        {
            Console.WriteLine("No male member found");
        }
        Console.WriteLine("Members are male:");
        maleMembers.ForEach(Console.WriteLine);
        Console.WriteLine(" ");
    }
    // Oldest member
    public void FindOldestMember(List<Member> members)
    {
        var oldestMember = members.MinBy(member => member.Dob);
        Console.WriteLine("Member is the oldest:");
        Console.WriteLine(oldestMember);
        Console.WriteLine(" ");
    }
    
    //FullName list
    public void PrintMemberDetails(List<Member> members)
    {
        if (members == null)
        {
            Console.WriteLine("No members to display.");
            return;
        }

        Console.WriteLine("FullName List:");
        var memberInfos = members.Select(member => new
        {
            FullName = member.LastName + " " + member.FirstName,
            member.Gender,
            Dob = member.Dob.ToString("d"),
            member.PhoneNumber,
            member.BirthPlace,
            member.Age,
            IsGraduated = (member.IsGraduated ? "Yes" : "No")
        }).ToList();
        memberInfos.ForEach(Console.WriteLine);
        Console.WriteLine(" ");
    }
    //Return 3 lists:
    //List of members who has birth year is 2000
    //List of members who has birth year greater than 2000
    //List of members who has birth year less than 2000
    public void PrintMembersInYear(List<Member> members)
    {
        var year = Convert.ToInt32(Console.ReadLine());
        var membersInYear = members.Where(member => member.Dob.Year == year).ToList();

        if (membersInYear.Count == 0)
        {
            Console.WriteLine($"No members were born in {year}.");
            return;
        }
        membersInYear.ForEach(Console.WriteLine);
    }

    public void PrintMembersAfterYear(List<Member> members)
    {
        var year = Convert.ToInt32(Console.ReadLine());
        var membersAfterYear = members.Where(member => member.Dob.Year > year).ToList();
        if (!membersAfterYear.Any())
        {
            Console.WriteLine($"No members were born after {year}.");
            return;
        }
        foreach (var member in membersAfterYear)
        {
            Console.WriteLine("Member: " + member);
        }
    }
    public void PrintMembersBeforeYear(List<Member> members)
    {
        var year = Convert.ToInt32(Console.ReadLine());
        var membersBeforeYear = members.Where(member => member.Dob.Year < year).ToList();
        if (!membersBeforeYear.Any())
        {
            Console.WriteLine($"No members were born before {year}.");
            return;
        }
        foreach (var member in membersBeforeYear)
        {
            Console.WriteLine("Member: " + member);
        }
    }

    //The first person born in Ha Noi
    public void FindFirstPersonFromHanoi(List<Member> members)
    {
        var firstPersonFromHanoi = members.FirstOrDefault(member => member.BirthPlace == "Ha Noi");
        if (firstPersonFromHanoi != null)
        {
            Console.WriteLine("The first person born in Ha Noi:");
            Console.WriteLine(firstPersonFromHanoi.ToString());
        }
        else
        {
            Console.WriteLine("No member was born in Ha Noi.");
        }
    }
    //Clear console
    public void ClearConsole()
    {
        Console.Clear();
    }
}