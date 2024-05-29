using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Domain.Models;

[Table("BookCategories")]
public class BookCategory
{
    [Key]
    public Guid BookCategoryId { get; set; }

    public Guid? BookId { get; set; }

    [Required]
    public Guid CategoryId { get; set; }

    [ForeignKey("BookId")]
    public Book? Book { get; set; }

    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
}