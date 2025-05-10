using Todo.Application.TodoTasks.DTOs;
using Todo.Application.TodoTasks.Mappings;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Shared;

namespace Todo.Application.TodoTasks.Commands.Create;

public class CreateTaskHandler
{
    private readonly ITaskRepository _taskRepository;
    public CreateTaskHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    public async Task<Result<TodoTaskDTO>> HandleAsync(CreateTaskCommand command)
    {
        TodoTask todoTask = new TodoTask(
            goal: command.Goal, 
            userId: command.UserId, 
            category: command.Category, 
            notes: command.Notes, 
            completationDate: command.CompletationDate
            );
        await _taskRepository.AddAsync(todoTask);
        var todoTaskDTO = TodoTaskMappingDTO.ToTaskDto(todoTask);
        
        return Result<TodoTaskDTO>.Ok(todoTaskDTO);
    }

}