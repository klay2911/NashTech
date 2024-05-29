using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Domain.Models;

[Table ("Books")]
public class Book
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string? Title { get; set; }
    
    [MaxLength(100)]
    public string? Author { get; set; }
    
    [MaxLength(50)]
    public string? Isbn { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }
    
    [MaxLength(150)]
    public string? CoverPath { get; set; }
    
    [MaxLength(150)]
    public string? BookPath { get; set; }
    
    public virtual ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();

    public virtual ICollection<BookBorrowingRequestDetails> BookBorrowingRequestDetails { get; set; } = new List<BookBorrowingRequestDetails>();
}