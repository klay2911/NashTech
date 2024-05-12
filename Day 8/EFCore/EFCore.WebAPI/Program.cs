using System.Text.Json.Serialization;
using EFCore.Models.Configs;
using EFCore.Services;
using EFCore.WebAPI.SeedData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionStrings = new ConnectionStrings();
builder.Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);

builder.Services.AddDbContext<CompanyContext>(options =>
{
    options.UseSqlServer(connectionStrings.DefaultConnection);
});
builder.Services.AddDbContext<CompanyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });


var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<CompanyContext>();

CompanyContextSeed.Seed(context);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
