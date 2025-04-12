using Recrute.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


using Recrute.Migrations;
using Microsoft.AspNetCore.Authentication.Cookies;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<RecruteDbService>();
    

// Add services
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("Connection");

builder.Services.AddDbContext<RecruteDbContext>(options =>
    options.UseMySql(
        connectionString,  // Get actual connection string
        new MySqlServerVersion(new Version(6, 0, 3)) // Specify your MySQL version
    )
);




builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
         .AllowAnyMethod()
        .AllowAnyHeader();

    });
});


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowReactApp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
