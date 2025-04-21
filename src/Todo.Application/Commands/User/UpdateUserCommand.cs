
namespace Todo.Application.Commands.User;

public class UpdateUserCommand
{
    public string? Name { get; init; }
    public string? Email { get; init; }
    public string? Password { get; init; }

    public UpdateUserCommand(string? name = null, string? email = null, string? password = null )
    {
        Name = name;
        Email = email;
        Password = password;
    }
}