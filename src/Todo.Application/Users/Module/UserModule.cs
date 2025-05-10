using Microsoft.Extensions.DependencyInjection;

using Todo.Application.Users.Commands.Create;
using Todo.Application.Users.Commands.Delete;
using Todo.Application.Users.Commands.Update;
using Todo.Application.Users.Queries.GetUser;

namespace Todo.Application.Users.Module;

public static class UserModule
{
    public static IServiceCollection AddUserModele(this IServiceCollection services)
    {
        services.AddScoped<CreateUserHandler>();
        services.AddScoped<DeleteUserHandler>();
        services.AddScoped<UpdateUserHandler>();
        
        services.AddScoped<GetUserHandler>();
        return services;
    }
}