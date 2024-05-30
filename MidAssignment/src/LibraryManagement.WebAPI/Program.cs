using LibraryManagement.Domain.Configs;
using LibraryManagement.Infrastructure;
using LibraryManagement.WebAPI.SeedData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionStrings = new ConnectionStrings();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);

builder.Services.AddDbContext<LibraryContext>(options =>
{
    options.UseSqlServer(connectionStrings.DefaultConnection);
});

builder.Services.AddControllers();

var app = builder.Build();
    
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
LibraryContextSeed.Seed(context, configuration);
    
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


