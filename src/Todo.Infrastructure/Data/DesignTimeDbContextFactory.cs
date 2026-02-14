using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Todo.Infrastructure.Data;
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
{
    public UserDbContext CreateDbContext(string[] args)
    {
        DotNetEnv.Env.Load(); 

        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                               ?? throw new InvalidOperationException("Variável DB_CONNECTION_STRING não encontrada.");

        var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new UserDbContext(optionsBuilder.Options);
    }
}