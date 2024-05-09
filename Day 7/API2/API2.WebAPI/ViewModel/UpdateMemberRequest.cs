using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using API.Models.Common;
using API.Models.Enum;

namespace API2.WebAPI.ViewModel;

public class UpdateMemberRequest
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
    
    /// <summary>
    /// Enter date of birth in format dd/MM/yyyy
    /// </summary>
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly? Dob { get; set; }
    
    /// <summary>
    /// GenderType is an enum with 4 values { 1: Unknown, 2: Male, 3: Female, 4: Other }
    /// </summary>
    [Required]
    public GenderType? Gender { get; set; }

    [Required]
    public string BirthPlace { get; set; }
    
    [RegularExpression("^[0-9]{10,11}$", ErrorMessage = "Phone number must be a number with 10 to 11 digits.")]
    public string PhoneNumber { get; set; }

    public bool? IsGraduated { get; set; }
}