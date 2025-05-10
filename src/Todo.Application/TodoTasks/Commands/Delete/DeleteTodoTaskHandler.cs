using Todo.Application.TodoTasks.Queries.GetOne;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Shared;

namespace Todo.Application.TodoTasks.Commands.Delete;

public class DeleteTodoTaskHandler
{
    private readonly ITodoTaskRepository _todoTaskRepository;
    private readonly GetTodoTaskHandler _todoTaskHandler;
    public DeleteTodoTaskHandler(ITodoTaskRepository todoTaskRepository, GetTodoTaskHandler todoTaskHandler)
    {
        _todoTaskRepository = todoTaskRepository;
        _todoTaskHandler = todoTaskHandler;
    }
    public async Task<Result<TodoTask>> HandleAsync(DeleteTodoTaskCommand command)
    {
        var query = new GetTodoTaskQuery(id: command.Id,userId: command.UserId);
        var verify = await  _todoTaskHandler.HandleAsync(query);
        
        if(verify is null)
            return Result<TodoTask>.Fail("task nao foi achado");

        await _todoTaskRepository.DeleteAsync(id: verify.Id);
        
        return Result<TodoTask>.Ok();
    }
}