namespace LibraryManagement.Domain.Models;

public class Base
{
    public bool IsDeleted { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string? CreatedBy { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public string? UpdateBy { get; set; }
}