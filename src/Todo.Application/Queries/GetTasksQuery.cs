namespace Todo.Application.Queries;

public class GetTasksQuery
{
    public Guid UserId { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init;}

  }