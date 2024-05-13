using EFCore.Repositories.Interfaces;
using EFCore.Services.Repositories;
using EFCore.Services.Services;

namespace EFCore.WebAPI.Configurations;

public static class ServiceConfiguration
{
    public static void ConfigureServiceLifetime(IServiceCollection services)
    {
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        
        services.AddScoped<IDepartmentService, DepartmentService>();
        
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        
        services.AddScoped<IEmployeeService, EmployeeService>();
        
        services.AddScoped<IProjectRepository, ProjectRepository>();
        
        services.AddScoped<IProjectService, ProjectService>();
        
        services.AddScoped<ISalaryRepository, SalaryRepository>();
        
        services.AddScoped<ISalaryService, SalaryService>();
        
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        
        services.AddScoped<IOrganizationService, OrganizationService>();
            
    }
}