using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.DTOs.AuthDTOs;

public class LoginRequest
{
    [Required, EmailAddress]

    public string? Email { get; set; } = string.Empty;
    [Required]
    public string? Password { get; set; } = string.Empty;
}