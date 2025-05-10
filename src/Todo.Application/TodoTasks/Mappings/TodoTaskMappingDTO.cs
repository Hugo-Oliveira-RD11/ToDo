using Todo.Application.TodoTasks.DTOs;
using Todo.Domain.Entities;

namespace Todo.Application.TodoTasks.Mappings;

public static class TodoTaskMappingDTO
{
    public static TaskDTO ToTaskDto(TodoTask task)
    {
        return new TaskDTO
        {
            Id = task.Id,
            Goal = task.Goal,
            Done = task.Done,
            Notes = task.Notes,
            Category = task.Category,
            CompletationDate = task.CompletationDate
        };
    }

    public static TodoTask ToTask(TaskDTO taskDto, Guid userId)
    {
        var task = TodoTask.LoadFromDb(
            id: taskDto.Id,
            userId: userId,
            goal: taskDto.Goal,
            notes: taskDto.Notes,
            category: taskDto.Category,
            completationDate: taskDto.CompletationDate.Value,
            done: taskDto.Done 
            );
        
        return task;
    }
}


























