using Todo.Domain.Enums;

namespace Todo.Domain.Entities;

public class TasksUsers
{
    public string Id { get; private set; } = string.Empty;
    public Guid UserId { get; private set; }
    public string Goal { get; private set; } = string.Empty;
    public string Notes { get; private set; } = string.Empty;
    public Category Category { get; private set; } = Category.White;
    public bool Done { get; private set; } = false;
    public DateTime? ADayToComplet { get; private set; } = null;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public TasksUsers() { } // Para o EF

    public TasksUsers(Guid userId, string goal, string notes, Category category, DateTime? aDayToComplete)
    {
        Id = Guid.NewGuid().ToString();
        UserId = userId;
        ValidateGoal(goal);
        Goal = goal;
        Notes = notes;
        Category = category;
        ADayToComplete = aDayToComplete;
        CreatedAt = DateTime.UtcNow;
    }
    public void UpdateGoal(string newGoal)
    {
        ValidateGoal(newGoal);
        Goal = newGoal;
    }

    public void UpdateNotes(string newNotes)
    {
        ValidateNotes(newNotes);
        Notes = newNotes;
    }

    public void UpdateCategory(Category newCategory) =>
        Category = newCategory;

    public void UpdateDayToComplete(DateTime? newDate)
    {
        ValidateDayToComplete(newDate);
        ADayToComplete = newDate;
    }

    public void MarkAsDone() =>
        Done = true;

    public void MarkAsNotDone() => 
        Done = false;

    private void ValidateGoal(string goal)
    {
        if (string.IsNullOrWhiteSpace(goal) || goal.Trim().Length < 3)
            throw new ArgumentException("O objetivo da tarefa deve ter pelo menos 3 caracteres.");
    }

    private void ValidateNotes(string notes)
    {
        if (!string.IsNullOrWhiteSpace(notes) && notes.Trim().Length < 1)
            throw new ArgumentException("Se preenchida, a anotação deve ter pelo menos 3 caracteres.");
    }

    private void ValidateDayToComplete(DateTime? date)
    {
        if (date.HasValue && date.Value.Date < DateTime.UtcNow.Date)
            throw new ArgumentException("A data de conclusão não pode ser no passado.");
    }
}