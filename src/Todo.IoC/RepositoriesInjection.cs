using Microsoft.Extensions.DependencyInjection;
using Todo.Domain.Interfaces.Repositories;
using Todo.Infrastructure.Data.Repositories;
using Todo.Infrastructure.Mapping;
using Todo.Infrastructure.Data;
using MongoDB.Driver;

namespace Todo.IoC
{
    public static class RepositoryInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();


            return services;
        }
    }
}