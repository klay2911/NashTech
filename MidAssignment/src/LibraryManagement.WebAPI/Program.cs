using LibraryManagement.Application.Mapper;
using LibraryManagement.Domain.Configs;
using LibraryManagement.Infrastructure;
using LibraryManagement.Infrastructure.Services;
using LibraryManagement.WebAPI.Configurations;
using LibraryManagement.WebAPI.SeedData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
//var connectionStrings = new ConnectionStrings();
var connectionStrings = new AppSettings();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//DbConnection and DBContext
builder.Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);

builder.Services.AddDbContext<LibraryContext>(options =>
{
    options.UseSqlServer(connectionStrings.DefaultConnection);
});

//AutoMapper
builder.Services.AddAutoMapper(typeof(BookProfile).Assembly);
builder.Services.AddAutoMapper(typeof(CategoryProfile).Assembly);
builder.Services.AddAutoMapper(typeof(RequestProfile).Assembly);
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);



//Add JWT Authentication
//builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.Cookie.Name = "YourAppCookieName";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.LoginPath = "/Account/Login";
})
.AddJwtBearer(options =>
 {
     options.RequireHttpsMetadata = false; 
     options.SaveToken = true; 
     options.TokenValidationParameters = TokenService.GetTokenValidationParameters(builder.Configuration);
 });

// builder.Services.AddAuthentication(option =>
//     {
//         option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     })
//     .AddJwtBearer(options =>
//     {
//         options.RequireHttpsMetadata = false; 
//         options.SaveToken = true; 
//         options.TokenValidationParameters = TokenService.GetTokenValidationParameters(builder.Configuration);
//     });

//Add Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", policy => policy.RequireRole("Administrator"));
    options.AddPolicy("Librarian", policy => policy.RequireRole("Librarian"));
    options.AddPolicy("Reader", policy => policy.RequireRole("Reader"));
});
builder.Services.AddSwaggerGen(opt =>
{
    var securitySchema = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    opt.AddSecurityDefinition("Bearer", securitySchema);
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securitySchema, new[] { "Bearer" } }
    });
});

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowOrigin", builder =>
//     {
//         builder.AllowAnyOrigin()
//             .AllowAnyMethod()
//             .AllowAnyHeader();
//     });
// });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
//Add services lifetime
ServiceConfiguration.ConfigureServiceLifetime(builder.Services);

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

app.UseStaticFiles();

//app.UseMiddleware<JwtMiddleware>();
app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


