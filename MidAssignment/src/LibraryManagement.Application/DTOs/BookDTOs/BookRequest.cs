using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.DTOs.BookDTOs;

public class BookRequest
{
    [Required]
    [StringLength(300, MinimumLength = 2)]
    public string? Title { get; set; }

    [StringLength(100)]
    public string? Author { get; set; }

    [StringLength(50)]
    public string? Isbn { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }
    
    public string? CoverPath { get; set; }

    public string? BookPath { get; set; }

    public Guid? CategoryId { get; set; }
}