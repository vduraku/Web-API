using Aralytiks.Domain.Entities;
using Aralytiks.Domain.Interfaces;
using Aralytiks.Domain.Interfaces.Services;
using Aralytiks.Infrastructure.Data;
using Aralytiks.Infrastructure.Mapping;
using Aralytiks.Infrastructure.Repositories;
using Aralytiks.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    // testd locally on my sql server instance
    options.UseSqlServer("Server=DESKTOP-K2LMBPP\\VALONA;Database=AralytiksDb;Trusted_Connection=True;TrustServerCertificate=True;"));

 //  when using docker
 // options.UseSqlServer("Server=localhost,1433;Database=AralytiksDb;User Id=sa;Password=Your_password123;TrustServerCertificate=True;")); 

// Register repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register mapping services
builder.Services.AddScoped<IPostMapper, PostMapper>();
builder.Services.AddScoped<IUserMapper, UserMapper>();

// Register business services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();

var app = builder.Build();

// Configure the HTTP requests
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//Check for database is created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

app.Run(); 