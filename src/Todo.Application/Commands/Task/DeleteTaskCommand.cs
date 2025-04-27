
namespace Todo.Application.Commands.Task;

public class DeleteTaskCommand
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }

    public DeleteTaskCommand(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}