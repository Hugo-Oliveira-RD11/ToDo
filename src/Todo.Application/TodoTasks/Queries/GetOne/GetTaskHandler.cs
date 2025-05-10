using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Entities;

namespace Todo.Application.TodoTasks.Queries.GetOne;
public class GetTaskHandler
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TodoTask?> HandleAsync(GetTaskQuery query)
    {
        if(query.Id.HasValue)
            return await _taskRepository.GetTaskByIdAndUserIdAsync(query.Id.Value.ToString(), query.UserId);
        
        if(!string.IsNullOrWhiteSpace(query.Title))
            return await _taskRepository.GetTaskByGoalAndUserIdAsync(query.Title!, query.UserId);
        
        return null;
    }

}