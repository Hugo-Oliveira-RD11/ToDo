
namespace Todo.Application.Users.Commands.Delete;

public class DeleteUserCommand
{
    public Guid Id { get; set; }
    public string? Email { get; set; }

}