
namespace Todo.Application.Users.Commands.Create;

public class CreateUserCommand
{
    public string Name { get; init; }
    public string Email { get; init; } 
    public string Password { get; init; }

    public CreateUserCommand(string name, string email, string password )
    {
        Name = name;
        Email = email;
        Password = password;
    }
}