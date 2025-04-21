namespace Todo.Application.Dtos;

public class UserDTO
{
    public Guid Id { get; init; }
    public string Name { get; private set; } = string.Empty;
    public Email Email { get; private set; } = Email.Empty;
}