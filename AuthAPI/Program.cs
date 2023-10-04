using AuthAPI.Data;
using AuthAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configs = builder.Configuration;

builder.Services.AddDbContext<AuthDBContext>(options =>
    options.UseSqlServer(configs.GetConnectionString("AuthDB")));

builder.Services
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My service");
        c.RoutePrefix = string.Empty;
    });
}
else
{
    app.MapGet("/", () => "AuthAPI");
}

app.MapControllers();

app.Run();
