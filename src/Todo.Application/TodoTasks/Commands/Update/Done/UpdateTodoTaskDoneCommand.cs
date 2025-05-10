namespace Todo.Application.TodoTasks.Commands.Update.Done;

public class UpdateTodoTaskDoneCommand
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }

    public UpdateTodoTaskDoneCommand(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}