using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Entities;
using Todo.Application.Queries;

namespace Todo.Application.Handlers.Tasks;
public class GetTasksQueryHandler
{
    private readonly ITaskRepository _taskRepository;

    public GetTasksQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<List<TodoTask?>> Handle(GetTasksQuery query)
    {
        var tasks = await _taskRepository.GetByUserIdPagedAsync(userId: query.UserId, pageNumber: query.PageNumber, pageSize: query.PageSize);
        return tasks.ToList();
    }

}