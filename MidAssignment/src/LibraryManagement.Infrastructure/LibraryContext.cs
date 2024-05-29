using LibraryManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure;

public class LibraryContext: DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<BookCategory> BookCategories { get; set; }
    public DbSet<BookBorrowingRequest> BookBorrowingRequests { get; set; }
    public DbSet<BookBorrowingRequestDetails> BookBorrowingRequestDetails { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.UserName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Email).IsRequired();
            entity.HasIndex(e=>e.UserName).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.Isbn).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.CoverPath).HasMaxLength(150);
            entity.Property(e => e.BookPath).HasMaxLength(150);
            entity.HasIndex(e=>e.Isbn).IsUnique();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId);
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<BookCategory>(entity =>
        {
            entity.HasKey(e => e.BookCategoryId);
            entity.HasOne(e => e.Book).WithMany(b => b.BookCategories).HasForeignKey(e => e.BookId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Category).WithMany(c => c.BookCategories).HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<BookBorrowingRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId);
            entity.Property(e => e.Requestor).IsRequired();
            entity.Property(e => e.DateRequested).IsRequired();
            entity.Property(e => e.Status).IsRequired();
            entity.HasOne(e => e.User).WithMany(u => u.BookBorrowingRequests).HasForeignKey(e => e.Requestor).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<BookBorrowingRequestDetails>(entity =>
        {
            entity.HasKey(e => e.RequestId);
            entity.Property(e => e.BookId).IsRequired();
            entity.HasOne(e => e.Book)
                .WithMany(b => b.BookBorrowingRequestDetails)
                .HasForeignKey(e => e.BookId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.BookBorrowingRequest)
                .WithMany(b => b.BookBorrowingRequestDetails)
                .HasForeignKey(e => e.RequestId);
        });
    }
    
}