using System;
using EFCore.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Services;

public class CompanyContext : DbContext
{
    public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department")
                .HasKey(a => a.Id);

            entity.Property(x => x.Name)
                .HasMaxLength(20);
            
        });
        
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee")
                .HasKey(a => a.Id);

            entity.Property(x => x.Name)
                .HasMaxLength(100) 
                .IsRequired();
        
            entity.Property(x => x.JoinedDate)
                .HasColumnType("date");
    
            entity.HasOne(e => e.Salary)
                .WithOne(s => s.Employee)
                .HasForeignKey<Salary>(s => s.EmployeeId);
    
            entity.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);
        });
        
        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Project")
                .HasKey(a => a.Id);

            entity.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();
            
            entity.HasMany(p => p.ProjectEmployees)
                .WithOne(pe => pe.Project)
                .HasForeignKey(pe => pe.ProjectId);
        });
        
        modelBuilder.Entity<Salary>(entity =>
        {
            entity.ToTable("Salary")
                .HasKey(a => a.Id);

            entity.Property(x => x.Wage)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });
        
        modelBuilder.Entity<ProjectEmployee>(entity =>
        {
            entity.ToTable("ProjectEmployee")
                .HasKey(a => new {a.ProjectId, a.EmployeeId});

            entity.Property(x => x.Enable);

            entity.HasOne(pe => pe.Project)
                .WithMany(p => p.ProjectEmployees)
                .HasForeignKey(pe => pe.ProjectId);
    
            entity.HasOne(pe => pe.Employee)
                .WithMany(e => e.ProjectEmployees)
                .HasForeignKey(pe => pe.EmployeeId);
        });

        modelBuilder.Entity<Department>().HasData(
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
    
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Salary> Salaries { get; set; }
    public DbSet<ProjectEmployee> ProjectEmployees { get; set; }
}