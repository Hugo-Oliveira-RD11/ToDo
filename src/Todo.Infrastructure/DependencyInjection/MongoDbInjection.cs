using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Todo.Infrastructure.Settings; 
using Todo.Domain.Interfaces.Repositories;
using Todo.Infrastructure.Repositories;


namespace Todo.IoC;

public static class MongoDbInjection
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("ConnectionsDB:TaskDatabase");

        var mongoDbSettings = section.Get<TasksModelDatabaseSettings>()
            ?? throw new InvalidOperationException("As configurações do MongoDB não foram encontradas no appsettings.json.");

        var connectionString = mongoDbSettings.ConnectionString
            ?? throw new InvalidOperationException("A ConnectionString do MongoDB está faltando no appsettings.json.");

        var databaseName = mongoDbSettings.DatabaseName
            ?? throw new InvalidOperationException("O DatabaseName do MongoDB está faltando no appsettings.json.");

        services.Configure<TasksModelDatabaseSettings>(section);

        services.AddSingleton<IMongoClient>(sp =>
        {
            return new MongoClient(connectionString);
        });

        services.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(databaseName);
        });

        services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();

        return services;
    }
}