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
    op => op.UseNpgsql(builder.Configuration["ConnectionsDB:UserConnection"])
);
builder.Services.Configure<TasksUsersDatabaseSettings>(
    builder.Configuration.GetSection("ConnectionsDB:TasksUsersDatabase"));

builder.Services.AddSingleton<TaskService>();
builder.Services.AddScoped<UserService>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UserContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();