using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Infrastructure.Data;

namespace Todo.Infrastructure.DependencyInjection;

public static class PostgresInjection
{
    public static IServiceCollection AddPostgresDb(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionsDB:UserConnection"]
                               ?? throw new InvalidOperationException("A string connection nao foi achada para o postgres");

        services.AddDbContext<UserDbContext>(options =>
        {
            options.UseNpgsql(connectionString); 
        });

        return services;
    }
}