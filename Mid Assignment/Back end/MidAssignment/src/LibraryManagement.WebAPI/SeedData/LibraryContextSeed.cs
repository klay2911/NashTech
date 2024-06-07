using LibraryManagement.Domain.Enum;
using LibraryManagement.Domain.Models;
using LibraryManagement.Infrastructure;
using LibraryManagement.Infrastructure.Helper;

namespace LibraryManagement.WebAPI.SeedData;

public static class LibraryContextSeed
{
    public static void Seed(LibraryContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            var authHelpers = new AuthHelpers();
            var users = new List<User>
            {
                new() { Email = "user1@email.com", Password = authHelpers.HashPassword("Password1"), FirstName = "First1", LastName = "Last1", Gender = GenderType.Male, Dob = new DateTime(1990, 1, 1), Role = Role.Librarian},
                new() { Email = "user2@email.com", Password = authHelpers.HashPassword("Password2"), FirstName = "First2", LastName = "Last2", Gender = GenderType.Male, Dob = new DateTime(1991, 1, 1), Role = Role.Librarian},
                new() { Email = "user3@email.com", Password = authHelpers.HashPassword("Password3"), FirstName = "First3", LastName = "Last3", Gender = GenderType.Female, Dob = new DateTime(1992, 1, 1), Role = Role.Reader},
                new() { Email = "user4@email.com", Password = authHelpers.HashPassword("Password4"), FirstName = "First4", LastName = "Last4", Gender = GenderType.Male, Dob = new DateTime(1993, 1, 1), Role = Role.Reader},
                new() { Email = "user5@email.com", Password = authHelpers.HashPassword("Password5"), FirstName = "First5", LastName = "Last5", Gender = GenderType.Female, Dob = new DateTime(1994, 1, 1), Role = Role.Reader},
                new() { Email = "user6@email.com", Password = authHelpers.HashPassword("Password6"), FirstName = "First6", LastName = "Last6", Gender = GenderType.Female, Dob = new DateTime(1995, 1, 1), Role = Role.Reader},
                new() { Email = "user7@email.com", Password = authHelpers.HashPassword("Password7"), FirstName = "First7", LastName = "Last7", Gender = GenderType.Female, Dob = new DateTime(1996, 1, 1), Role = Role.Reader}
            };
            context.Users.AddRange(users);
            context.SaveChanges();
        }

        if (context.Categories.Any()) return;
        var categories = new List<Category>
        {
            new() { Name = "Fiction", CreatedAt = DateTime.Now },
            new() { Name = "NonFiction", CreatedAt = DateTime.Now },
            new() { Name = "Science", CreatedAt = DateTime.Now },
            new() { Name = "Technology", CreatedAt = DateTime.Now},
            new() { Name = "History", CreatedAt = DateTime.Now}
        };
        context.Categories.AddRange(categories);
        context.SaveChanges();

        if (context.Books.Any()) return;
        var books = new List<Book>
        {
            new() { Title = "Book1", Author = "Author1", Isbn = "ISBN1", Description = "Description1", CategoryId = categories[0].CategoryId, CreatedBy = "La Vu", CoverPath = "/covers/Cover.png", BookPath = "/pdfs/1.pdf", CreatedAt = DateTime.Now},
            new() { Title = "Book2", Author = "Author2", Isbn = "ISBN2", Description = "Description2", CategoryId = categories[1].CategoryId, CreatedBy = "La Vu", CoverPath = "/covers/Cover.png", BookPath = "/pdfs/2.pdf", CreatedAt = DateTime.Now},
            new() { Title = "Book3", Author = "Author3", Isbn = "ISBN3", Description = "Description3", CategoryId = categories[2].CategoryId, CreatedBy = "La Vu", CoverPath = "/covers/Cover.png", BookPath = "/pdfs/3.pdf", CreatedAt = DateTime.Now},
            new() { Title = "Book4", Author = "Author4", Isbn = "ISBN4", Description = "Description4", CategoryId = categories[3].CategoryId, CreatedBy = "La Vu", CoverPath = "/covers/Cover.png", BookPath = "/pdfs/4.pdf", CreatedAt = DateTime.Now},
            new() { Title = "Book5", Author = "Author5", Isbn = "ISBN5", Description = "Description5", CategoryId = categories[4].CategoryId, CreatedBy = "La Vu", CoverPath = "/covers/Cover.png", BookPath = "/pdfs/5.pdf", CreatedAt = DateTime.Now},
            new() { Title = "Book6", Author = "Author6", Isbn = "ISBN6", Description = "Description6", CategoryId = categories[0].CategoryId, CreatedBy = "La Vu", CoverPath = "/covers/Cover.png", BookPath = "/pdfs/6.pdf", CreatedAt = DateTime.Now},
            new() { Title = "Book7", Author = "Author7", Isbn = "ISBN7", Description = "Description7", CategoryId = categories[1].CategoryId, CreatedBy = "La Vu", CoverPath = "/covers/Cover.png", BookPath = "/pdfs/7.pdf", CreatedAt = DateTime.Now},
            new() { Title = "Book8", Author = "Author8", Isbn = "ISBN8", Description = "Description8", CategoryId = categories[2].CategoryId, CreatedBy = "La Vu", CoverPath = "/covers/Cover.png", BookPath = "/pdfs/8.pdf", CreatedAt = DateTime.Now},
            new() { Title = "Book9", Author = "Author9", Isbn = "ISBN9", Description = "Description9", CategoryId = categories[3].CategoryId, CreatedBy = "La Vu", CoverPath = "/covers/Cover.png", BookPath = "/pdfs/9.pdf", CreatedAt = DateTime.Now},
            new() { Title = "Book10", Author = "Author10", Isbn = "ISBN10", Description = "Description10", CategoryId = categories[4].CategoryId, CreatedBy = "La Vu", CoverPath = "/covers/Cover.png", BookPath = "/pdfs/10.pdf", CreatedAt = DateTime.Now}
        };
        context.Books.AddRange(books);
        context.SaveChanges();
    }
}