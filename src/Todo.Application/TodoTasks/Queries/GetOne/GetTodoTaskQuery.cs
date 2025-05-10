namespace Todo.Application.TodoTasks.Queries.GetOne;

public class GetTodoTaskQuery
{
    public Guid? Id { get; init; }
    public Guid  UserId { get; init; }
    public string? Title { get; init; }

    public GetTodoTaskQuery(Guid id, Guid userId ,string? title = null)
    {
        Id = id;
        UserId = userId;
        Title = title;
    }
}