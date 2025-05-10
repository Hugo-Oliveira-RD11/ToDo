using Todo.Application.TodoTasks.Module;
using Todo.Application.Users.Module;
using Todo.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddPostgresDb(builder.Configuration);
builder.Services.AddRepositories();

builder.Services.AddUserModele();
builder.Services.AddTodoTaskModule();

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
