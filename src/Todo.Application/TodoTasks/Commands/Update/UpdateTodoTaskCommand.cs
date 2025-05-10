using Todo.Domain.Enums;
using Todo.Domain.ValueObjects;

namespace Todo.Application.TodoTasks.Commands.Update;

public class UpdateTodoTaskCommand
{
    public string? Goal { get; init; } 
    public string? Notes { get; init; } 
    public Categories? Category { get; init; } 
    public bool? Done { get; init; } 
    public DueDate? CompletationDate { get; init; }

    public UpdateTodoTaskCommand(string? goal = null, string? notes = null, Categories? category = null, bool? done = false, DueDate? completationDate = null)
    {
        Goal = goal;
        Notes = notes;
        Category = category;
        Done = done;
        CompletationDate = completationDate;
    }
}