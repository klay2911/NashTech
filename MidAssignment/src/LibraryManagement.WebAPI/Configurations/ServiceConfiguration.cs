using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.Infrastructure.Repositories;
using LibraryManagement.Infrastructure.Services;

namespace LibraryManagement.WebAPI.Configurations;

public static class ServiceConfiguration
{
    public static void ConfigureServiceLifetime(IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookService, BookService>();
    }
}