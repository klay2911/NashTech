using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using API.Models.Common;
using API.Models.Enum;
using API.Models.Validation;

namespace API.Models.Models;

public class Member
{
    public Guid Id { get; set; }
    
    [Required]
    [MinLength(2)]
    [DisplayName("First Name")]
    public string FirstName { get; set; }
    
    [Required]
    [MinLength(2)]
    [DisplayName("Last Name")]
    public string LastName { get; set; }
    
    [DisplayName("Full Name")]
    public string FullName => FirstName + " " + LastName;
    
    [CustomValidation.CheckGender]
    public GenderType Gender { get; set; }
    
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly Dob { get; set; }
    
    [DisplayName("Phone Number")]
    public string PhoneNumber { get; set; }
    
    public string BirthPlace { get; set; }

    private int Age => DateTime.Now.Year - Dob.Year;
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