using Todo.Application.Commands.Task;
using Todo.Application.Handlers.Tasks;
using Todo.Application.Queries;
using Todo.Domain.Interfaces.Repositories;

public class UpdateTaskTitleHandler
{
    private readonly ITaskRepository _taskRepository;
    private readonly GetTaskQueryHandler _taskQueryHandler;

    public UpdateTaskTitleHandler(ITaskRepository taskRepository, GetTaskQueryHandler taskQueryHandler)
    {
        _taskRepository = taskRepository;
        _taskQueryHandler = taskQueryHandler;
    }

    public async Task HandleAsync(UpdateTaskTitleCommand command)
    {
        var task = await _taskQueryHandler.HandleAsync(new GetTaskQuery(id: command.TaskId, userId: command.UserId, title: command.NewTitle));

        if (task is null)
            throw new ArgumentException("Task não encontrada.");

        task.UpdateGoal(command.NewTitle);

        await _taskRepository.UpdateAsync(task);
    }
}