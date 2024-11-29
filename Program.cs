using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Users_Api.Models;
using Users_Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddPooledDbContextFactory<UserDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IUserRepository, UserRepository>();

// Configurar Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Users API", Version = "v1" });
});

var app = builder.Build();

// Habilitar Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users API v1");
    c.RoutePrefix = string.Empty; // Esto hace que Swagger UI esté disponible en la raíz
});

// Configure the HTTP request pipeline.
app.UseCors("AllowApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
