using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Domain.Enum;

namespace LibraryManagement.Domain.Models;

[Table("User")]
public class User : BaseModel
{
    [Key]
    public Guid UserId { get; set; }
    
    [Required]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    
    [MaxLength(30)]
    public string? FirstName { get; set; }

    [MaxLength(30)]
    public string? LastName { get; set; }

    [Required]
    public GenderType? Gender { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }

    [Required]
    public Role? Role { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime Dob { get; set; }
    
    public int Age => DateTime.Now.Year - Dob.Year;
    
    public ICollection<BookBorrowingRequest> BookBorrowingRequests { get; } = new List<BookBorrowingRequest>();
}