
using Todo.Domain.Enums;
using Todo.Domain.ValueObjects;

namespace Todo.Application.Commands.Task;

public class CreateTaskCommand
{
    public string Goal { get; init; }
    public string Notes { get; init; }
    public Categories Category { get; init; }
    public bool Done { get; init; } 
    public DueDate CompletationDate { get; init ; }

    public CreateTaskCommand(string goal, string notes, Categories category, bool done, DueDate completationDate)
    {
        Goal = goal;
        Notes = notes;
        Category = category;
        Done = done;
        CompletationDate = completationDate;
    }
}