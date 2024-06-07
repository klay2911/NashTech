using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.DTOs.AuthDTOs;

public class RegisterRequest
{

    
    [Required, EmailAddress]
    public string? Email { get; set; } = string.Empty;
    
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public string? Password { get; set; } = string.Empty;

    [Required, Compare(nameof(Password))] public string? ConfirmPassword { get; set; } = string.Empty;
}