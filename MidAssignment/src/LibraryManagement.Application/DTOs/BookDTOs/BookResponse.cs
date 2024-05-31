namespace LibraryManagement.Application.DTOs.BookDTOs;

public class BookResponse
{
    public Guid Id { get; set; }
    
    public string? Title { get; set; }
    
    public string? Author { get; set; }
    
    public string? Isbn { get; set; }
    
    public string? Description { get; set; }
    
    public string? CoverPath { get; set; }
    
    public string? BookPath { get; set; }
    
    public Guid? CategoryId { get; set; }
}