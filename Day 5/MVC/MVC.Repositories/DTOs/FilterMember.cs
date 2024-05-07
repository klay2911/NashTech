using MVC.Models.Enum;

namespace MVC.Repositories.DTOs;

public class FilterMember
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    
    public GenderType? Gender { get; set; }
    public DateOnly? Dob { get; set; }
    public string? PhoneNumber { get; set; }
    public string? BirthPlace { get; set; }
    public bool? IsGraduated { get; set; }
}