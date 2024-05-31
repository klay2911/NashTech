using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Domain.Models;

[Table ("BookBorrowingRequestDetails")]
public class BookBorrowingRequestDetails : BaseModel
{
    [Key]
    public Guid RequestId { get; set; }
    
    [Required]
    public Guid BookId { get; set; }
    
    public int? LastReadPageNumber { get; set; } 
    
    [ForeignKey("BookId")]
    public Book? Book { get; set; }
    
    [ForeignKey("RequestId")]
    public BookBorrowingRequest? BookBorrowingRequest { get; set; }
}