
namespace Todo.Application.Users.Commands.Delete;

public class UpdateUserCommand
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Email { get; init; }
    public string? Password { get; init; }

    public UpdateUserCommand(Guid id, string? name = null, string? email = null, string? password = null )
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }
}