using EFCore.Models.Models;
using EFCore.Services;

namespace EFCore.WebAPI.SeedData;

public static class CompanyContextSeed
{
    public static void Seed(CompanyContext context)
    {
        context.Database.EnsureCreated();

        if (context.Departments.Any()) return;
        context.Departments.AddRange(
            new Department
            {
                Id = Guid.NewGuid(),
                Name = "Software development"
            },
            new Department
            {
                Id = Guid.NewGuid(),
                Name = "Finance"
            },
            new Department
            {
                Id = Guid.NewGuid(),
                Name = "Accountant"
            },
            new Department
            {
                Id = Guid.NewGuid(),
                Name = "HR"
            }
        );

        context.SaveChanges();
    }
}