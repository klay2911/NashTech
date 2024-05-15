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
            context.SaveChanges();
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
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = "Alice Smith",
                    DepartmentId = softwareDept.Id,
                    JoinedDate = DateOnly.FromDateTime(DateTime.Now)
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = "Bob Johnson",
                    DepartmentId = financeDept.Id,
                    JoinedDate = DateOnly.FromDateTime(DateTime.Now)
                }
            );
            context.SaveChanges();
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
            context.SaveChanges();
        }
        if (!context.Salaries.Any())
        {
            var john = context.Employees.FirstOrDefault(e => e.Name == "John Doe");
            var jane = context.Employees.FirstOrDefault(e => e.Name == "Jane Doe");
            var alice = context.Employees.FirstOrDefault(e => e.Name == "Alice Smith");
            var bob = context.Employees.FirstOrDefault(e => e.Name == "Bob Johnson");
            var west = context.Employees.FirstOrDefault(e => e.Name == "West Doe");
            var janeMark = context.Employees.FirstOrDefault(e => e.Name == "Jane Mark");

            var salaries = new List<Salary>();

            if (john != null)
            {
                salaries.Add(new Salary
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = john.Id,
                    Wage = 80
                });
            }

            if (jane != null)
            {
                salaries.Add(new Salary
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = jane.Id,
                    Wage = 120
                });
            }

            if (alice != null)
            {
                salaries.Add(new Salary
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = alice.Id,
                    Wage = 70
                });
            }

            if (bob != null)
            {
                salaries.Add(new Salary
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = bob.Id,
                    Wage = 150
                });
            }
            if (west != null)
            {
                salaries.Add(new Salary
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = west.Id,
                    Wage = 100
                });
            }

            if (janeMark != null)
            {
                salaries.Add(new Salary
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = janeMark.Id,
                    Wage = 110
                });
            }

            context.Salaries.AddRange(salaries);
            context.SaveChanges();
        }
        context.SaveChanges();
    }
}