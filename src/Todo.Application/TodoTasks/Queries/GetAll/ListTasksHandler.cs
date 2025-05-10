using Todo.Domain.Entities;
using Todo.Domain.Interfaces.Repositories;

namespace Todo.Application.TodoTasks.Queries.GetAll;

public class ListTasksHandler
{
    private readonly ITaskRepository _taskRepository;

    public ListTasksHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<TodoTask?>> HandleAsync(ListTasksQuery query)
    {
        return await _taskRepository.GetByUserIdPagedAsync(query.UserId, query.PageNumber, query.PageSize);
    }
}