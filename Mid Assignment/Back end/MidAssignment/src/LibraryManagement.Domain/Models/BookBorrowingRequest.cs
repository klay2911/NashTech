using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Domain.Enum;

namespace LibraryManagement.Domain.Models;

[Table("BookBorrowingRequests")]
public class BookBorrowingRequest : BaseModel
{
    [Key]
    public Guid RequestId { get; set; }
    
    public Guid? RequestorId { get; set; }
    
    public DateTime? DateRequested { get; set; }
    
    public RequestStatus? Status { get; set; }
    
    public Guid? ApproverId { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime? ExpiryDate { get; set; }
    
    [ForeignKey("RequestorId")]
    public User? User { get; set; }
    
    [ForeignKey("ApproverId")]
    public User? Approver { get; set; }
    
    public ICollection<BookBorrowingRequestDetails> BookBorrowingRequestDetails { get; set; } = new List<BookBorrowingRequestDetails>();
}