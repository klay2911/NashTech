using EFCore.Models.Models;
using EFCore.Services;

namespace EFCore.WebAPI.SeedData;

public static class CompanyContextSeed
{
    public static void Seed(CompanyContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Departments.Any())
        {
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
        }

        if (!context.Employees.Any())
        {
            var softwareDept = context.Departments.First(d => d.Name == "Software development");
            var financeDept = context.Departments.First(d => d.Name == "Finance");

            context.Employees.AddRange(
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = "John Doe",
                    DepartmentId = softwareDept.Id,
                    JoinedDate = DateOnly.FromDateTime(DateTime.Now)
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = "Jane Doe",
                    DepartmentId = financeDept.Id,
                    JoinedDate = DateOnly.FromDateTime(DateTime.Now)
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = "Jane Mark",
                    DepartmentId = softwareDept.Id,
                    JoinedDate = DateOnly.FromDateTime(DateTime.Now)
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = "West Doe",
                    DepartmentId = financeDept.Id,
                    JoinedDate = DateOnly.FromDateTime(DateTime.Now)
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = "Ted Doe",
                    DepartmentId = softwareDept.Id,
                    JoinedDate = DateOnly.FromDateTime(DateTime.Now)
                }
            );
        }
        if (!context.Projects.Any())
        {
            context.Projects.AddRange(
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Project 1",
                },
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Project 2",
                },
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Project 3",
                },
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Project 4",
                },
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Project 5",
                }
            );
        }
        if (!context.Salaries.Any())
        {
            var john = context.Employees.First(e => e.Name == "John Doe");
            var jane = context.Employees.First(e => e.Name == "Jane Doe");
            var janeMark = context.Employees.First(e => e.Name == "Jane Mark");
            var west = context.Employees.First(e => e.Name == "West Doe");
            var ted = context.Employees.First(e => e.Name == "Ted Doe");
            context.Salaries.Add(
                new Salary
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = john.Id,
                    Wage = 80
                }
            );
            context.Salaries.Add(
                new Salary
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = jane.Id,
                    Wage = 120
                }
            );
        }
        context.SaveChanges();
    }
}