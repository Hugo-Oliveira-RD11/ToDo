using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Todo.Domain.Interfaces.Repositories;
using Todo.Infrastructure.Data;
using Todo.Infrastructure.Data.Repositories;
using Todo.Infrastructure.Settings;

namespace Todo.IoC;

public static class MongoDbInjection
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("ConnectionsDB:TaskDatabase");

        var mongoDbSettings = section.Get<MongoDbSettings>()
            ?? throw new InvalidOperationException("As configurações do MongoDB não foram encontradas no appsettings.json.");

        var connectionString = mongoDbSettings.ConnectionString
            ?? throw new InvalidOperationException("A ConnectionString do MongoDB está faltando no appsettings.json.");

        var databaseName = mongoDbSettings.DatabaseName
            ?? throw new InvalidOperationException("O DatabaseName do MongoDB está faltando no appsettings.json.");

        services.Configure<MongoDbSettings>(section);

        services.AddSingleton<IMongoClient>(sp =>
        {
            return new MongoClient(connectionString);
        });

        services.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(databaseName);
        });

        services.AddScoped<ITaskRepository, TaskRepository>();

        return services;
    }
}