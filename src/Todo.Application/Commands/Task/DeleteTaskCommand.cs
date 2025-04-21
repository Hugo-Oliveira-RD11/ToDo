
namespace Todo.Application.Commands.Task;

public class DeleteTaskCommand
{
    public int Id { get; init; }

    public DeleteTaskCommand(int id)
    {
        Id = id;
    }
}