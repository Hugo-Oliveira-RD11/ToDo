
namespace Todo.Application.Commands.User;

public class DeleteUserCommand
{
    public Guid Id { get; init; }
    public Email Email { get; init; } 

    public DeleteUserCommand(Guid? id = null, Email? email = Email.Empty)
    {
        Email = email;
        Id = id;
    }
}