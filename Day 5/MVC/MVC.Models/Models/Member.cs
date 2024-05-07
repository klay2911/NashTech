using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MVC.Models.Enum;

namespace MVC.Models.Models;

public class Member
{
    public Guid Id { get; set; }
    
    [DisplayName("First Name")]
    public string FirstName { get; set; }
    
    [Required]
    [DisplayName("Last Name")]
    public string LastName { get; set; }
    
    [DisplayName("Full Name")]
    public string FullName => FirstName + " " + LastName;
    
    public GenderType Gender { get; set; }
    
    public DateOnly Dob { get; set; }
    
    [DisplayName("Phone Number")]
    public string PhoneNumber { get; set; }
    
    public string BirthPlace { get; set; }
    
    public int Age => DateTime.Now.Year - Dob.Year;
    [DisplayName ("Is Graduated")]
    public bool IsGraduated { get; set; }
    
    public Member()
    {
        Id = Guid.NewGuid();
    }
    public Member( string firstName, string lastName, GenderType gender, DateOnly dob, string phoneNumber, string birthPlace, bool isGraduated)
    {
        Id = Guid.NewGuid();
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
        return "Id" + Id + " | First Name:" + FirstName + " | Last Name:" + LastName + " | Gender:" + Gender + " | Dob:" + Dob.ToString("dd/MM/yyyy") +
               " | Phone Number:" + PhoneNumber + " | Birth Place:" + BirthPlace + " | Age:" + Age +
               " | Is Graduated:" + (IsGraduated ? "Yes" : "No");
    }
}