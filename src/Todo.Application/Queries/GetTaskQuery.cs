namespace Todo.Application.Queries;

public class GetTaskQuery
{
    public Guid? Id { get; init; }
    public string? Title { get; init; }

    public GetTaskQuery(Guid? id = null, string? title = null)
    {
        Id = id;
        Title = title;
    }
}