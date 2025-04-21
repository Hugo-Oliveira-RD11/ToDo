
namespace Todo.Application.Commands.User;

public class UpdateUserCommand
{
    public string Name { get; init; }
    public Email Email { get; init; }
    public string Password { get; init; }

    public UpdateUserCommand(string? name = string.Empty, Email? email = Email.Empty, string? password = string.Empty )
    {
        Name = name;
        Email = email;
        Password = password;
    }
}