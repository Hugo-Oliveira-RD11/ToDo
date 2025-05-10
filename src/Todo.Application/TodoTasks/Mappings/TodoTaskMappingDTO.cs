using Todo.Application.TodoTasks.DTOs;
using Todo.Domain.Entities;

namespace Todo.Application.TodoTasks.Mappings;

public static class TodoTaskMappingDTO
{
    public static TodoTaskDTO ToTaskDto(TodoTask task)
    {
        return new TodoTaskDTO
        {
            Id = task.Id,
            Goal = task.Goal,
            Done = task.Done,
            Notes = task.Notes,
            Category = task.Category,
            CompletationDate = task.CompletationDate
        };
    }

    public static TodoTask ToTask(TodoTaskDTO todoTaskDto, Guid userId)
    {
        var task = TodoTask.LoadFromDb(
            id: todoTaskDto.Id,
            userId: userId,
            goal: todoTaskDto.Goal,
            notes: todoTaskDto.Notes,
            category: todoTaskDto.Category,
            completationDate: todoTaskDto.CompletationDate.Value,
            done: todoTaskDto.Done 
            );
        
        return task;
    }
}


























