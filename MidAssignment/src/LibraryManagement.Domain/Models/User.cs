using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Domain.Enum;

namespace LibraryManagement.Domain.Models;

[Table("User")]
public class User
{
    [Key]
    public Guid UserId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string? UserName { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    
    [MaxLength(50)]
    public string? FirstName { get; set; }

    [MaxLength(50)]
    public string? LastName { get; set; }

    public GenderType? Gender { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }

    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    public Role? Role { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime Dob { get; set; }
    
    public int Age => DateTime.Now.Year - Dob.Year;
    
    public ICollection<BookBorrowingRequest> BookBorrowingRequests { get; } = new List<BookBorrowingRequest>();
}