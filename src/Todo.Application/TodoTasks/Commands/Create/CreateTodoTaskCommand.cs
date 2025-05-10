
using Todo.Domain.Enums;
using Todo.Domain.ValueObjects;

namespace Todo.Application.TodoTasks.Commands.Create;

public class CreateTodoTaskCommand
{
    public string Goal { get; init; }
    public string Notes { get; init; }
    public Guid UserId { get; init; }
    public Categories Category { get; init; }
    public bool Done { get; init; } 
    public DueDate CompletationDate { get; init ; }

    public CreateTodoTaskCommand(string goal, Guid userId,
        DueDate completationDate, bool done = false, string notes = null, Categories category = Categories.White)
    {
        Goal = goal;
        UserId = userId;
        Notes = notes;
        Category = category;
        Done = done;
        CompletationDate = completationDate;
    }
}