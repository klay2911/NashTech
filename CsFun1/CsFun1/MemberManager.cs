namespace CsFun1;

public class MemberManager
{
    // Male member
    public void PrintMaleMembers(List<Member> members)
        {
            //Member maleMembers = null;
            Console.WriteLine("Members are male:");
            foreach (Member member in members)
            {
                if (member.Gender == "Nam")
                    Console.WriteLine(member.ToString());
            }
            Console.WriteLine(" ");
        }
    // Oldest member
    public void FindOldestMember(List<Member> members)
    {
        Member oldestMember = members[0];

        for (int i = 1; i < members.Count; i++)
        {
            if (members[i].Dob < oldestMember.Dob)
            {
                oldestMember = members[i];
            }
        }

        Console.WriteLine("Oldest member:");
        Console.WriteLine(oldestMember);
        Console.WriteLine(" ");
    }
    
    //FullName list
    public void PrintMemberDetails(List<Member> members)
    {
        Console.WriteLine("Fullname List:");
        foreach (Member member in members)
        {
            Console.WriteLine("Full Name: " + member.FullName);
            Console.WriteLine("Gender: " + member.Gender);
            Console.WriteLine("Date of Birth: " + member.Dob.ToShortDateString());
            Console.WriteLine("Phone Number: " + member.PhoneNumber);
            Console.WriteLine("Birth Place: " + member.BirthPlace);
            Console.WriteLine("Age: " + member.Age);
            Console.WriteLine("Is Graduated: " + member.IsGraduated);
            Console.WriteLine(" ");
        }
    }
    //Return 3 lists:
    //List of members who has birth year is 2000
    //List of members who has birth year greater than 2000
    //List of members who has birth year less than 2000
    public void PrintMembersInYear(List<Member> members)
    {
        var year = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($"\nMembers born in {year}:");
        foreach (Member member in members)
        {
            if (member.Dob.Year == year)
            {
                Console.WriteLine(member);
            }
        }
    }

    public void PrintMembersAfterYear(List<Member> members)
    {
        var year = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($"\nMembers born after {year}:");
        foreach (Member member in members)
        {
            if (member.Dob.Year > year)
            {
                Console.WriteLine(member);
            }
        }
    }
    public void PrintMembersBeforeYear(List<Member> members)
    {
        var year = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($"\nMembers born before {year}:");
        foreach (Member member in members)
        {
            if (member.Dob.Year < year)
            {
                Console.WriteLine(member);
            }
        }
    }

    //The first person born in Ha Noi
    public void FindFirstPersonFromHanoi(List<Member> members)
    {
        Member firstPersonFromHanoi = null;
        foreach (var member in members)
        {
            if (member.BirthPlace == "Ha Noi")
            {
                firstPersonFromHanoi = member;
                break;
            }
        }
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