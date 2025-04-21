using Todo.Domain.Entities;
using Todo.Domain.ValueObjects;
using Todo.Infrastructure.Data.Model;

namespace Todo.Infrastructure.Mapping;

public static class TaskMapping
{
    public static TodoTaskModel ToTaskModel(TodoTask task)
    {
        return new TodoTaskModel
        {
            Id = task.Id,
            UserId = task.UserId,
            Goal = task.Goal,
            Notes = task.Notes,
            Category = task.Category,
            Done = task.Done,
            CompletationDate = task.CompletationDate?.Value,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static TodoTask ToTask(TodoTaskModel taskModel)
    {
        return new TodoTask(
            taskModel.UserId,
            taskModel.Goal,
            taskModel.Notes,
            taskModel.Category,
            taskModel.CompletationDate.HasValue ? new DueDate(taskModel.CompletationDate.Value) : null
        );
    }
}