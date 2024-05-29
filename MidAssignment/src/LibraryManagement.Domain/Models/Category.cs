using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Domain.Enum;

namespace LibraryManagement.Domain.Models;

[Table("Categories")]
public class Category
{
    [Key]
    public Guid CategoryId { get; set; }
    
    [Required]
    public CategoryName? Name { get; set; }
    
    public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
}