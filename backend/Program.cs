using backend.Data;
using backend.Models;
using backend.Services;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<UserContext>(
    op => op.UseNpgsql(builder.Configuration.GetConnectionString("UserConnection"))
);
builder.Services.Configure<TasksUsersDatabaseSettings>(
    builder.Configuration.GetSection("TasksConnection"));

builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<UserService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();