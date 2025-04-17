using backend.Services.TaskServices;
using Microsoft.Extensions.DependencyInjection;

namespace backend.IoC;
public static class  DependencyInjectionTask
{
    public static IServiceCollection AddTaskServices(this IServiceCollection services)
    {
        services.AddScoped<ITaskService, TaskService>();

        return services;
    }
}