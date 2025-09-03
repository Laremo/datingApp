using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// CORS Policy.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient",
       policy =>
       {
           policy.WithOrigins("http://localhost:4200")
                 .AllowAnyHeader()
                 .AllowAnyMethod();
       });
});

builder.Services.AddDbContext<AppDataContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

var app = builder.Build();

app.UseCors("AllowClient");

app.UseAuthorization();

app.MapControllers();

app.Run();
