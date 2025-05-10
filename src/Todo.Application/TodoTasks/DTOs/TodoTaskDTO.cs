using Todo.Domain.Enums;
using Todo.Domain.ValueObjects;

namespace Todo.Application.TodoTasks.DTOs;

public class TodoTaskDTO
{
    public string Id { get; set; } = string.Empty;
    public string Goal { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public Categories Category { get; set; } = Categories.White;
    public bool Done { get; set; } = false;
    public DueDate CompletationDate { get; set; }
}