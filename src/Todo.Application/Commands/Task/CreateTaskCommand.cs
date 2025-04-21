
namespace Todo.Application.Commands.Task;

public class CreateTaskCommand
{
    public string Goal { get; init; }
    public string Notes { get; init; }
    public Category Category { get; init; }
    public bool Done { get; init; } 
    public DueDate ADayToComplet { get; init ; }

    public CreateTaskCommand(string goal, string notes, Category category, bool done, DueDate aDayToComplet)
    {
        Goal = goal;
        Notes = notes;
        Category = category;
        Done = done;
        ADayToComplet = aDayToComplet;
    }
}