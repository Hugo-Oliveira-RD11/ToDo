using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

using MongoDB.Driver;

using Todo.Api.Modules;
using Todo.Infrastructure.Data;
using Todo.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddPostgresDb(builder.Configuration);
builder.Services.AddRepositories();

builder.Services.AddUserModele();
builder.Services.AddTodoTaskModule();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration["ConnectionsDB:UserConnection"]!, name: "database")
    .AddMongoDb(sp => new MongoClient(builder.Configuration["ConnectionsDB:TasksDatabase:ConnectionString"]!), name: "mongodb");

var app = builder.Build();

app.MapGet("/", () =>
{

    var hostmane = Environment.MachineName;
    return $"Ola mundo {hostmane}";
});

using (var scope = app.Services.CreateScope())
{
    int maxRetries = 10, delayMilliseconds = 5000;

    var db = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            db.Database.Migrate();
            break;
        }
        catch (Exception e)
        {
            Console.WriteLine($"error: {e}");
            Thread.Sleep(delayMilliseconds);
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
// app.MapHealthChecks(
//     "/v1/health", new HealthCheckOptions()
//     {
//         Predicate = _ => true,
//         ReponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
//     });

app.Run();