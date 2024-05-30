using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Domain.Enum;

namespace LibraryManagement.Domain.Models;

[Table("BookBorrowingRequests")]
public class BookBorrowingRequest
{
    [Key]
    public Guid RequestId { get; set; }
    
    public Guid? Requestor { get; set; }
    
    public DateTime? DateRequested { get; set; }
    
    public RequestStatus? Status { get; set; }
    
    public Guid? Approver { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime? ExpiryDate { get; set; }
    
    [ForeignKey("UserId")]
    public User? User { get; set; }
    
    public ICollection<BookBorrowingRequestDetails> BookBorrowingRequestDetails { get; set; } = new List<BookBorrowingRequestDetails>();
}