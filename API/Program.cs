using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();


builder.Services.AddDbContext<AppDataContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));
builder.Services.AddScoped<ITokenService, TokenService>();
var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader()
.AllowAnyMethod()
.WithOrigins(
"https://localhost:4200",
"http://localhost:4200"
));

app.UseAuthorization();

app.MapControllers();

app.Run();
