using Todo.Application.TodoTasks.Queries.GetOne;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Shared;

namespace Todo.Application.TodoTasks.Commands.Delete;

public class DeleteTodoTaskHandler
{
    private readonly ITaskRepository _taskRepository;
    private readonly GetTaskHandler _taskHandler;
    public DeleteTodoTaskHandler(ITaskRepository taskRepository, GetTaskHandler taskHandler)
    {
        _taskRepository = taskRepository;
        _taskHandler = taskHandler;
    }
    public async Task<Result<TodoTask>> HandleAsync(DeleteTodoTaskCommand command)
    {
        var query = new GetTaskQuery(id: command.Id,userId: command.UserId);
        var verify = await  _taskHandler.HandleAsync(query);
        
        if(verify is null)
            return Result<TodoTask>.Fail("task nao foi achado");

        await _taskRepository.DeleteAsync(id: verify.Id);
        
        return Result<TodoTask>.Ok();
    }
}