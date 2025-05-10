using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Entities;

namespace Todo.Application.TodoTasks.Queries.GetOne;
public class GetTodoTaskHandler
{
    private readonly ITodoTaskRepository _todoTaskRepository;

    public GetTodoTaskHandler(ITodoTaskRepository todoTaskRepository)
    {
        _todoTaskRepository = todoTaskRepository;
    }

    public async Task<TodoTask?> HandleAsync(GetTodoTaskQuery query)
    {
        if(query.Id.HasValue)
            return await _todoTaskRepository.GetTaskByIdAndUserIdAsync(query.Id.Value.ToString(), query.UserId);
        
        if(!string.IsNullOrWhiteSpace(query.Title))
            return await _todoTaskRepository.GetTaskByGoalAndUserIdAsync(query.Title!, query.UserId);
        
        return null;
    }

}