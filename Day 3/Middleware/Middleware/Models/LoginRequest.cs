using System.ComponentModel.DataAnnotations;

namespace Middleware.Models;

public class LoginRequest
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Message { get; set; }
}