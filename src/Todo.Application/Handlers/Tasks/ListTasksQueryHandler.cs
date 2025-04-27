using Todo.Application.Queries;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces.Repositories;

namespace Todo.Application.Handlers.Tasks;

public class ListTasksQueryHandler
{
    private readonly ITaskRepository _taskRepository;

    public ListTasksQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<TodoTask?>> HandleAsync(ListTasksQuery query)
    {
        return await _taskRepository.GetByUserIdPagedAsync(query.UserId, query.PageNumber, query.PageSize);
    }
}