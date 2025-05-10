using Todo.Application.TodoTasks.DTOs;
using Todo.Application.TodoTasks.Mappings;
using Todo.Application.TodoTasks.Queries.GetOne;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Shared;

namespace Todo.Application.TodoTasks.Commands.Update.Done;

public class UpdateTodoTaskDoneHandler
{
    private readonly ITodoTaskRepository _todoTaskRepository;
    private readonly GetTodoTaskHandler _todoTaskHandler;
    public UpdateTodoTaskDoneHandler(ITodoTaskRepository todoTaskRepository, GetTodoTaskHandler todoTaskHandler)
    {
        _todoTaskRepository = todoTaskRepository;
        _todoTaskHandler = todoTaskHandler;
    }

    public async Task<Result<TodoTaskDTO>> HandleAsync(UpdateTodoTaskDoneCommand command)
    {
        var query = new GetTodoTaskQuery(id: command.Id, userId: command.UserId);
        TodoTask? todoTask = await _todoTaskHandler.HandleAsync(query); 
        if(todoTask is null)
            return Result<TodoTaskDTO>.Fail("");
        
        if(!todoTask.Done)
            todoTask.MarkAsDone();
        else
            todoTask.MarkAsNotDone();

        await _todoTaskRepository.UpdateAsync(todoTask: todoTask);
        
        var todoTaskDTO = TodoTaskMappingDTO.ToTaskDto(todoTask); 
        return Result<TodoTaskDTO>.Ok(todoTaskDTO);
    }
}