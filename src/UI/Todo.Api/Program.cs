
using Microsoft.EntityFrameworkCore;
using HealthChecks.MongoDb;
using HealthChecks.NpgSql;

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
    .AddCheck("self", () => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy())
    .AddNpgSql(
        connectionString: builder.Configuration["ConnectionsDB:UserConnection"]!,
        name: "Postgres",
        tags: new[] { "database", "sql" })
    .AddMongoDb( sp => new MongoClient(builder.Configuration["ConnectionsDB:TasksDatabase:ConnectionString"]),
        name: "TaskDB-Mongo"
        );

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    int maxRetries = 10, delayMilliseconds=5000;

    var db = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    for(int i=0; i< maxRetries;i++ ){
        try{
            db.Database.Migrate();
            break;
        }
        catch(Exception e){
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
app.MapHealthChecks(
    "/v1/health",new HealthCheckOptions()
    {
        Predicate = _ => true,
        ReponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();