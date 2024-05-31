using LibraryManagement.Domain.Enum;
using LibraryManagement.Domain.Models;
using LibraryManagement.Infrastructure;
using LibraryManagement.WebAPI.Helpers;

namespace LibraryManagement.WebAPI.SeedData;

public static class LibraryContextSeed
{
    public static void Seed(LibraryContext context, IConfiguration configuration)
    {
        context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            var authHelpers = new AuthHelpers(configuration, context);
            var users = new List<User>
            {
                new() { Email = "user1@email.com", Password = authHelpers.HashPassword("Password1"), FirstName = "First1", LastName = "Last1", Gender = GenderType.Male, Dob = new DateTime(1990, 1, 1), Role = Role.Administrator},
                new() { Email = "user2@email.com", Password = authHelpers.HashPassword("Password2"), FirstName = "First2", LastName = "Last2", Gender = GenderType.Male, Dob = new DateTime(1991, 1, 1), Role = Role.Librarian},
                new() { Email = "user3@email.com", Password = authHelpers.HashPassword("Password3"), FirstName = "First3", LastName = "Last3", Gender = GenderType.Female, Dob = new DateTime(1992, 1, 1), Role = Role.Librarian},
                new() { Email = "user4@email.com", Password = authHelpers.HashPassword("Password4"), FirstName = "First4", LastName = "Last4", Gender = GenderType.Male, Dob = new DateTime(1993, 1, 1), Role = Role.Reader},
                new() { Email = "user5@email.com", Password = authHelpers.HashPassword("Password5"), FirstName = "First5", LastName = "Last5", Gender = GenderType.Female, Dob = new DateTime(1994, 1, 1), Role = Role.Reader}
            };
            context.Users.AddRange(users);
            context.SaveChanges();
        }

        if (context.Books.Any()) return;
        var books = new List<Book>
        {
            new() { Title = "Book1", Author = "Author1", Isbn = "ISBN1", Description = "Description1" },
            new() { Title = "Book2", Author = "Author2", Isbn = "ISBN2", Description = "Description2" },
            new() { Title = "Book3", Author = "Author3", Isbn = "ISBN3", Description = "Description3" },
            new() { Title = "Book4", Author = "Author4", Isbn = "ISBN4", Description = "Description4" },
            new() { Title = "Book5", Author = "Author5", Isbn = "ISBN5", Description = "Description5" }
        };
        context.Books.AddRange(books);
        context.SaveChanges();

        if (!context.Categories.Any())
        {
            var categories = new List<Category>
            {
                new() { Name = "Fiction" },
                new() { Name = "NonFiction" },
                new() { Name = "Science" },
                new() { Name = "Technology" },
                new() { Name = "History" }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }
        context.SaveChanges();

    }
}