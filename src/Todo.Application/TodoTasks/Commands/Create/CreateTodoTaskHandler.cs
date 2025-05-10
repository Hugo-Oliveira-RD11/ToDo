using Todo.Application.TodoTasks.DTOs;
using Todo.Application.TodoTasks.Mappings;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Shared;

namespace Todo.Application.TodoTasks.Commands.Create;

public class CreateTodoTaskHandler
{
    private readonly ITodoTaskRepository _todoTaskRepository;
    public CreateTodoTaskHandler(ITodoTaskRepository todoTaskRepository)
    {
        _todoTaskRepository = todoTaskRepository;
    }
    public async Task<Result<TodoTaskDTO>> HandleAsync(CreateTodoTaskCommand command)
    {
        TodoTask todoTask = new TodoTask(
            goal: command.Goal, 
            userId: command.UserId, 
            category: command.Category, 
            notes: command.Notes, 
            completationDate: command.CompletationDate
            );
        await _todoTaskRepository.AddAsync(todoTask);
        var todoTaskDTO = TodoTaskMappingDTO.ToTaskDto(todoTask);
        
        return Result<TodoTaskDTO>.Ok(todoTaskDTO);
    }

}