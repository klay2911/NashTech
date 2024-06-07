using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Domain.Models;

[Table ("Books")]
public class Book : BaseModel
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(300)]
    public string? Title { get; set; }
    
    [MaxLength(100)]
    public string? Author { get; set; }
    
    [MaxLength(50)]
    public string? Isbn { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }
    
    public string? CoverPath { get; set; }
    
    public string? BookPath { get; set; }
    
    public Guid CategoryId { get; set; }
    
    public Category? Category { get; set; }
    
    //public virtual ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();

    public virtual ICollection<BookBorrowingRequestDetails> BookBorrowingRequestDetails { get; set; } = new List<BookBorrowingRequestDetails>();
}