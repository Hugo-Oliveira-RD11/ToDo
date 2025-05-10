namespace Todo.Application.TodoTasks.Commands.Update.Done;

public class UpdateTaskDoneCommand
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }

    public UpdateTaskDoneCommand(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}