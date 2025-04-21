namespace Todo.Application.Commands.Task;

public class UpdateTaskCommand
{
    public string Goal { get; init; } 
    public string Notes { get; init; } 
    public Category Category { get; init; } 
    public bool Done { get; init; } 
    public DueDate ADayToComplet { get; init; }

    public UpdateTaskCommand(string? goal = string.Empty, string? notes = string.Empty, Category? category = null, bool? done = false, DueDate? aDayToComplet = null)
    {
        Goal = goal;
        Notes = notes;
        Category = category;
        Done = done;
        ADayToComplet = aDayToComplet;
    }
}