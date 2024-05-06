using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class Person
{
    //[RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
    public Guid Id { get; set; }
    
    [DisplayName("First Name")]
    public string FirstName { get; set; }
    
    [Required]
    [DisplayName("Last Name")]
    public string LastName { get; set; }
    
    public string FullName => FirstName + " " + LastName;
    
    public GenderType Gender { get; set; }
    
    public DateTime Dob { get; set; }
    [DisplayName("Phone Number")]
    public string PhoneNumber { get; set; }
    
    public string BirthPlace { get; set; }
    
    public int Age => DateTime.Now.Year - Dob.Year;
    [DisplayName ("Is Graduated")]
    public bool IsGraduated { get; set; }
    
    public Person()
    {
        Id = Guid.NewGuid();
    }

    public Person(string firstName, string lastName, GenderType gender, DateTime dob, string phoneNumber, string birthPlace, bool isGraduated)
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