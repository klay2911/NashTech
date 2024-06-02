using LibraryManagement.Application.DTOs.BookDTOs;
using LibraryManagement.Domain.Enum;

namespace LibraryManagement.Application.DTOs.BorrowingRequestDTOs;

public class BorrowingRequestResponse
{
    public Guid RequestId { get; set; }
    
    public Guid? RequestorId { get; set; }
    
    public DateTime? DateRequested { get; set; }
    
    public RequestStatus? Status { get; set; }

    //public Guid? ApproverId { get; set; }
    
    public List<BookResponse> Books { get; set; } = new();

}