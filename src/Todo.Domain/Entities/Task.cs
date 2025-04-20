
namespace Todo.Domain.Entities;

using Todo.Domain.ValueObjects;  // Para usar DueDate

public class Task
{
    public string Id { get; private set; } = string.Empty;
    public Guid UserId { get; private set; }
    public string Goal { get; private set; } = string.Empty;
    public string Notes { get; private set; } = string.Empty;
    public Category Category { get; private set; } = Category.White;
    public bool Done { get; private set; } = false;
    public DueDate? ADayToComplete { get; private set; } = null;

    public Task() { } // Para o EF

    public Task(Guid userId, string goal, string notes, Category category, DueDate? aDayToComplete)
    {
        Id = Guid.NewGuid().ToString();
        UserId = userId;
        ValidateGoal(goal);
        Goal = goal;
        Notes = notes;
        Category = category;
        ADayToComplete = aDayToComplete;
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

    public void UpdateDayToComplete(DueDate? newDate)
    {
        ADayToComplete = newDate;  // Atualiza com o novo DueDate
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
}