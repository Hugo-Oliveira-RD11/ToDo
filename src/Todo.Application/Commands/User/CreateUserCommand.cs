
namespace Todo.Application.Commands.User;

public class CreateUserCommand
{
    public string Name { get; init; }
    public Email Email { get; init; } 
    public string Password { get; init; }

    public CreateUserCommand(string name, Email email, string password )
    {
        Name = name;
        Email = email;
        Password = password;
    }
}