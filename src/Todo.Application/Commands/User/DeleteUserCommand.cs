
namespace Todo.Application.Commands.User;

public class DeleteUserCommand
{
    public Guid Id { get; set; }
    public string? Email { get; set; }

}