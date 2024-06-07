using System.Text.Json.Serialization;
using LibraryManagement.Application.Common.Converter;
using LibraryManagement.Application.DTOs.UserDTOs;
using LibraryManagement.Domain.Enum;

namespace LibraryManagement.Application.DTOs.BorrowingRequestDTOs;

public class BorrowingRequestResponse
{
    public Guid RequestId { get; set; }
    
    //public Guid? RequestorId { get; set; }
    
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? DateRequested { get; set; }
    
    public RequestStatus? Status { get; set; }

    //public Guid? ApproverId { get; set; }
    
    public UserResponse? User { get; set; }
    
    public UserResponse? Approver { get; set; }

    public List<Guid> BookIds { get; set; } = new();

}