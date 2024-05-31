using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Domain.Models;

[Table("Categories")]
public class Category : BaseModel
{
    [Key]
    public Guid CategoryId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string? Name { get; set; }
    
    public ICollection<Book> Books { get; } = new List<Book>();
}