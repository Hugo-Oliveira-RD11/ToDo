using Todo.Application.TodoTasks.Commands.Create;
using Todo.Application.TodoTasks.Commands.Delete;
using Todo.Application.TodoTasks.Commands.Update;
using Todo.Application.TodoTasks.Commands.Update.Done;
using Todo.Application.TodoTasks.Commands.Update.Title;
using Todo.Application.TodoTasks.Queries.GetAll;
using Todo.Application.TodoTasks.Queries.GetOne;

namespace Todo.Api.Modules;

public static class TodoTaskModule
{
    public static IServiceCollection AddTodoTaskModule(this IServiceCollection services)
    {
        services.AddScoped<CreateTodoTaskHandler>();

        services.AddScoped<DeleteTodoTaskHandler>();
        
        services.AddScoped<UpdateTodoTaskHandler>();
        services.AddScoped<UpdateTodoTaskDoneHandler>();
        services.AddScoped<UpdateTodoTaskTitleHandler>();

        services.AddScoped<ListTodoTasksHandler>();
        services.AddScoped<GetTodoTaskHandler>();
        
        return services;
    }
}