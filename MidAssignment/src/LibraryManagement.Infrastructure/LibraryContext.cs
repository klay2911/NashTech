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
    
    public DbSet<BookBorrowingRequest> BookBorrowingRequests { get; set; }
    
    public DbSet<BookBorrowingRequestDetails> BookBorrowingRequestDetails { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Gender).IsRequired();
            entity.Property(e => e.PhoneNumber).HasMaxLength(11);
            entity.Property(e => e.Role).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(300);
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.Isbn).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.CoverPath);
            entity.Property(e => e.BookPath);
            entity.HasIndex(e=>e.Isbn).IsUnique();
            entity.HasOne(a => a.Category)
                .WithMany(b => b.Books)
                .HasForeignKey(a => a.CategoryId)
                .IsRequired();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId);
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<BookBorrowingRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId);
            entity.Property(e => e.RequestorId);
            entity.Property(e => e.DateRequested);
            entity.Property(e => e.Status);
            entity.HasOne(e => e.User).WithMany(u => u.BookBorrowingRequests).HasForeignKey(e => e.RequestorId);
        });

        modelBuilder.Entity<BookBorrowingRequestDetails>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.RequestId).IsRequired();
            entity.Property(e => e.BookId).IsRequired();
            entity.HasOne(e => e.Book)
                .WithMany(b => b.BookBorrowingRequestDetails)
                .HasForeignKey(e => e.BookId);
            entity.HasOne(e => e.BookBorrowingRequest)
                .WithMany(b => b.BookBorrowingRequestDetails)
                .HasForeignKey(e => e.RequestId);
        });
    }
    
}