namespace Todo.Application.Queries;

public class GetUserQuery
{
    public Guid? Id { get; init; }
    public string? Email { get; init; }

    public GetUserQuery(Guid? id = null, string? email = null)
    {
        Id = id;
        Email = email;
    }
}