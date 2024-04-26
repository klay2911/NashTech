// See https://aka.ms/new-console-template for more information
namespace CsFun2;

public class Member
{
    public String FirstName { get; }
    public String LastName { get; set; }
    public String FullName => FirstName + " " + LastName;
    public String Gender { get; set; }
    public DateTime Dob { get; set; }
    public String PhoneNumber { get; set; }
    public String BirthPlace { get;}
    public int Age => DateTime.Now.Year - Dob.Year;
    public bool IsGraduated { get; }
    public Member(string firstName, string lastName, string gender, DateTime dob, string phoneNumber, string birthPlace, bool isGraduated)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Dob = dob;
        PhoneNumber = phoneNumber;
        BirthPlace = birthPlace;
        IsGraduated = isGraduated;
    }
    public override string ToString()
    {
        return "First Name:" + FirstName + " | Last Name:" + LastName + " | Gender:" + Gender + " | Dob:" + Dob.ToString("dd/MM/yyyy") +
               " | Phone Number:" + PhoneNumber + " | Birth Place:" + BirthPlace + " | Age:" + Age +
               " | Is Graduated:" + (IsGraduated ? "Yes" : "No");
    }
}