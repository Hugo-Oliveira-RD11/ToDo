using Todo.Domain.Entities;

namespace Todo.Application.Mapping;

public static class TaskMappingDTO
{
    public static TaskDto ToTaskDto(Task task)
    {
        return new TaskDto
        {
            Id = task.Id,
            Objetivo = task.Objetivo,
            Notas = task.Notas,
            Category = task.Category,
            Feito = task.Feito,
            ADayToComplet = task.ADayToComplet,
            CreatedAt = task.CreatedAt
        };
    }

    public static Task ToTask(TaskDto taskDto, Guid userId)
    {
        return new Task(
            userId,
            taskDto.Objetivo,
            taskDto.Notas,
            taskDto.Category,
            taskDto.ADayToComplet
        )
        {
            Id = taskDto.Id,
            Feito = taskDto.Feito,
            CreatedAt = taskDto.CreatedAt
        };
    }
}