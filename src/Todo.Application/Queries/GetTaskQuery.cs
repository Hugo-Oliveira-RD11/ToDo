namespace Todo.Application.Queries;

public class GetTasksQuery
{
    public int Page { get; }
    public int PageSize { get; }

    public GetTasksQuery(int page = 1, int pageSize = 10)
    {
        Page = page < 1 ? 1 : page;
        PageSize = pageSize;
    }

    public bool IsPaged => PageSize > 0;
}