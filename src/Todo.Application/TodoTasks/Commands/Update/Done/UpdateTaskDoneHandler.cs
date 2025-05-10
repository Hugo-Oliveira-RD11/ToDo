using Todo.Application.TodoTasks.DTOs;
using Todo.Application.TodoTasks.Mappings;
using Todo.Application.TodoTasks.Queries.GetOne;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Shared;

namespace Todo.Application.TodoTasks.Commands.Update.Done;

public class UpdateTaskDoneHandler
{
    private readonly ITaskRepository _taskRepository;
    private readonly GetTaskHandler _taskHandler;
    public UpdateTaskDoneHandler(ITaskRepository taskRepository, GetTaskHandler taskHandler)
    {
        _taskRepository = taskRepository;
        _taskHandler = taskHandler;
    }

    public async Task<Result<TodoTaskDTO>> HandleAsync(UpdateTaskDoneCommand command)
    {
        var query = new GetTaskQuery(id: command.Id, userId: command.UserId);
        TodoTask? todoTask = await _taskHandler.HandleAsync(query); 
        if(todoTask is null)
            return Result<TodoTaskDTO>.Fail("");
        
        if(!todoTask.Done)
            todoTask.MarkAsDone();
        else
            todoTask.MarkAsNotDone();

        await _taskRepository.UpdateAsync(todoTask: todoTask);
        
        var todoTaskDTO = TodoTaskMappingDTO.ToTaskDto(todoTask); 
        return Result<TodoTaskDTO>.Ok(todoTaskDTO);
    }
}