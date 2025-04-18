using Mapster;
using Todo.Domain.Entities;
using Todo.Infrastructure.Models;

public static class MappingConfig
{
    public static void RegisterMappingsToUsers()
    {
        TypeAdapterConfig<User, UserModel>.NewConfig();
    }

    public static void RegisterMappingsToTask()
    {
        TypeAdapterConfig<User, UserModel>.NewConfig();
    }
}