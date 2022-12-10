using ConcurrencyAPI.Models;
using ConcurrencyAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(options => options.WithOrigins(new[] { "http://localhost:3000", "http://localhost:8080", "http://localhost:4200" }).AllowAnyHeader().AllowAnyMethod().AllowCredentials());

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
	app.Run();
}
else
{
	app.Run("http://localhost:8000");
}