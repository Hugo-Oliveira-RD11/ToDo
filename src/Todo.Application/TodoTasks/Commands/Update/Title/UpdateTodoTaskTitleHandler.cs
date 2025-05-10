using Todo.Application.TodoTasks.Queries.GetOne;
using Todo.Domain.Interfaces.Repositories;

namespace Todo.Application.TodoTasks.Commands.Update.Title;
public class UpdateTodoTaskTitleHandler
{
    private readonly ITodoTaskRepository _todoTaskRepository;
    private readonly GetTodoTaskHandler _todoTaskHandler;

    public UpdateTodoTaskTitleHandler(ITodoTaskRepository todoTaskRepository, GetTodoTaskHandler todoTaskHandler)
    {
        _todoTaskRepository = todoTaskRepository;
        _todoTaskHandler = todoTaskHandler;
    }

    public async Task HandleAsync(UpdateTodoTaskTitleCommand command)
    {
        var query = new GetTodoTaskQuery(id: command.TaskId, userId: command.UserId, title: command.NewTitle);
        var task = await _todoTaskHandler.HandleAsync(query);

        if (task is null)
            throw new ArgumentException("Task não encontrada.");

        task.UpdateGoal(command.NewTitle);

        await _todoTaskRepository.UpdateAsync(task);
    }
}