using Microsoft.Extensions.DependencyInjection;
using Todo.Domain.Interfaces.Repositories;
using Todo.Infrastructure.Repositories;

namespace Todo.Infrastructure.DependencyInjection;
    public static class RepositoryInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();


            return services;
        }
    }