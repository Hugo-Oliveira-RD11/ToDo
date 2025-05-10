using Todo.Application.TodoTasks.Queries.GetOne;
using Todo.Domain.Interfaces.Repositories;

namespace Todo.Application.TodoTasks.Commands.Update.Title;
public class UpdateTaskTitleHandler
{
    private readonly ITaskRepository _taskRepository;
    private readonly GetTaskHandler _taskHandler;

    public UpdateTaskTitleHandler(ITaskRepository taskRepository, GetTaskHandler taskHandler)
    {
        _taskRepository = taskRepository;
        _taskHandler = taskHandler;
    }

    public async Task HandleAsync(UpdateTaskTitleCommand command)
    {
        var task = await _taskHandler.HandleAsync(new GetTaskQuery(id: command.TaskId, userId: command.UserId, title: command.NewTitle));

        if (task is null)
            throw new ArgumentException("Task não encontrada.");

        task.UpdateGoal(command.NewTitle);

        await _taskRepository.UpdateAsync(task);
    }
}