namespace CsFun1;

public class Menu
{
    public void DisplayMenu()
    {
        List<Member> members = new List<Member>
        {
            new Member("Nguyen Van", "Nam", "Nam", new DateTime(1999, 06, 02), "0945628812", "VietNam", true),
            new Member("Do Tuan", "Duc", "Nam", new DateTime(2000, 11, 08), "0938428762", "Ha Noi", false),
            new Member("Hoang Thanh", "Huong", "Nu", new DateTime(2002, 4, 20), "0948348712", "VietNam", false)
        };


        MemberManager manager = new MemberManager();

        Dictionary<int, Action> mainMenuActions = new Dictionary<int, Action>
        {
            { 1, () => manager.PrintMaleMembers(members) },
            { 2, () => manager.FindOldestMember(members) },
            { 3, () => manager.PrintMemberDetails(members) },
            { 5, () => manager.FindFirstPersonFromHanoi(members) },
            { 6, () => manager.ClearConsole() },
        };

        Dictionary<int, Action> subMenuActions = new Dictionary<int, Action>
        {
            { 1, () => manager.PrintMembersInYear(members) },
            { 2, () => manager.PrintMembersAfterYear(members) },
            { 3, () => manager.PrintMembersBeforeYear(members) },
        };

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Male members");
            Console.WriteLine("2. Oldest member");
            Console.WriteLine("3. FullName members");
            Console.WriteLine("4. Get member born base on year");
            Console.WriteLine("5. First person born in Ha Noi: ");
            Console.WriteLine("6. Clear console: ");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");

            string? input = Console.ReadLine();
            if (!int.TryParse(input, out var choice))
            {
                Console.WriteLine("Invalid input. Please try again.");
                continue;
            }

            if (choice == 7)
            {
                exit = true;
                continue;
            }

            if (choice == 4)
            {
                bool exit1 = false;
                while (!exit1)
                {
                    Console.WriteLine("Menu:");
                    Console.WriteLine("1. Members in Year");
                    Console.WriteLine("2. Members after Year");
                    Console.WriteLine("3. Members before Year");
                    Console.WriteLine("4. Exit");
                    var input1 = Console.ReadLine();
                    if (!int.TryParse(input1, out var choices))
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                        continue;
                    }

                    if (choices == 4)
                    {
                        exit1 = true;
                        continue;
                    }

                    if (subMenuActions.TryGetValue(choices, out var action))
                    {
                        Console.Write("Enter year:");
                        action();
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please try again.");
                    }
                }
            }
            else if (mainMenuActions.TryGetValue(choice, out var action))
            {
                action();
            }
            else
            {
                Console.WriteLine("Please choose number from 1 to 7.");
            }
        }
    }
}