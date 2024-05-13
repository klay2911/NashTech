using System.Reflection;
using System.Text.Json.Serialization;
using EFCore.Models.Configs;
using EFCore.Services;
using EFCore.WebAPI.Configurations;
using EFCore.WebAPI.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Member API", Version = "v1" });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
var connectionStrings = new ConnectionStrings();
builder.Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);

builder.Services.AddDbContext<CompanyContext>(options =>
{
    options.UseSqlServer(connectionStrings.DefaultConnection);
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
ServiceConfiguration.ConfigureServiceLifetime(builder.Services);

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
app.MapControllers();

app.Run();
