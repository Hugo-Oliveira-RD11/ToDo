
namespace Todo.Application.TodoTasks.Commands.Delete;

public class DeleteTodoTaskCommand
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }

    public DeleteTodoTaskCommand(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}