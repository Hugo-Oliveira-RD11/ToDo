
using Todo.Domain.Enums;

using DueDate = Todo.Domain.ValueObjects.DueDate;

namespace Todo.Application.DTOs;

public class TaskDTO
{
    public string Id { get; set; } = string.Empty;
    public string Goal { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public Categories Category { get; set; } = Categories.White;
    public bool Done { get; set; } = false;
    public DueDate CompletationDate { get; set; }
}