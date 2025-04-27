namespace Todo.Application.Queries;

public class GetTasksQuery
{
    public Guid UserId { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init;}

  }