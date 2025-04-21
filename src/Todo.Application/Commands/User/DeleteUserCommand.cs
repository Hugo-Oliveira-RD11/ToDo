
namespace Todo.Application.Commands.User;

public class DeleteUserCommand
{
    public Guid Id { get; init; }
    public string? Email { get; init; } 

    public DeleteUserCommand(Guid? id = null, string? email = null)
    {
        Email = email;
        Id = (Guid)id!;
    }
}