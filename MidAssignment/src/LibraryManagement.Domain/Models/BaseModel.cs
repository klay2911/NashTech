namespace LibraryManagement.Domain.Models;

public class BaseModel
{
    public bool IsDeleted { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string? CreatedBy { get; set; }
    
    public DateTime ModifyAt { get; set; }
    
    public string? ModifyBy { get; set; }
}