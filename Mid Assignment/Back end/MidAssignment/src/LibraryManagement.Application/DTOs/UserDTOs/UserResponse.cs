using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.DTOs.UserDTOs;

public class UserResponse
{
    public Guid UserId { get; set; }
    
    public string? LastName { get; set; }
    
    public string? Email { get; set; }
    
    //public string? PhoneNumber { get; set; }
    
    public int Age { get; set; }

}