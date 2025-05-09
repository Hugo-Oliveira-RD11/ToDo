namespace Todo.Application.Queries;

public class ListTasksQuery
{
    public Guid UserId { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init;}

    public ListTasksQuery(Guid userId ,int page = 1, int pageSize = 20)
    {
        UserId = userId;
        PageNumber = page < 1 ? 1 : page;
        PageSize = pageSize;
    }

    public bool IsPaged => PageSize > 0;
}