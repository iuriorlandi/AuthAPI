using AuthAPI.Data;
using AuthAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configs = builder.Configuration;

builder.Services.AddDbContext<AuthDBContext>(options =>
    options.UseNpgsql(configs.GetConnectionString("AuthDB")));

builder.Services.AddCors();

builder.Services
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IUserService, UserService>()
    .AddScoped<IAuthService, AuthService>()
    .AddScoped<ITokenService, TokenService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My service");
    c.RoutePrefix = string.Empty;
});



app.MapControllers();

app.Run();
