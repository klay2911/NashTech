using LibraryManagement.Application.Interfaces.Helpers;
using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.Infrastructure.Helper;
using LibraryManagement.Infrastructure.Repositories;
using LibraryManagement.Infrastructure.Services;
namespace LibraryManagement.WebAPI.Configurations;

public static class ServiceConfiguration
{
    public static void ConfigureServiceLifetime(IServiceCollection services)
    {
        services.AddScoped<IAuthHelper, AuthHelpers>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IBookBorrowingRequestRepository, BookBorrowingRequestRepository>();
        services.AddScoped<IBookBorrowingRequestDetailsRepository, BookBorrowingRequestDetailsRepository>();
        services.AddScoped<IBookBorrowingRequestService, BookBorrowingService>();
        
    }
}