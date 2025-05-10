using Todo.Domain.Entities;
using Todo.Domain.Interfaces.Repositories;

namespace Todo.Application.TodoTasks.Queries.GetAll;

public class ListTodoTasksHandler
{
    private readonly ITodoTaskRepository _todoTaskRepository;

    public ListTodoTasksHandler(ITodoTaskRepository todoTaskRepository)
    {
        _todoTaskRepository = todoTaskRepository;
    }

    public async Task<IEnumerable<TodoTask?>> HandleAsync(ListTodoTasksQuery query)
    {
        return await _todoTaskRepository.GetByUserIdPagedAsync(query.UserId, query.PageNumber, query.PageSize);
    }
}