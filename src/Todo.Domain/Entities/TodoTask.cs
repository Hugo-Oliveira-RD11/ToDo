using Todo.Domain.Enums;
using Todo.Domain.ValueObjects;

namespace Todo.Domain.Entities;
// Para usar DueDate

public class TodoTask
{
    public string Id { get; private set; } 
    public Guid UserId { get; private set; }
    public string Goal { get; private set; } = string.Empty;
    public string Notes { get; private set; } = string.Empty;
    public Categories Category { get; private set; } 
    public bool Done { get; private set; } = false;
    public DueDate CompletationDate { get; private set; }

    public TodoTask() { } // Para o EF

    public TodoTask(Guid userId, string goal, string notes, Categories category, DueDate completationDate)
    {
        ValidateGoal(goal);
        ValidateNotes(notes);
        
        Id = Guid.NewGuid().ToString();
        UserId = userId;
        Goal = goal;
        Notes = notes;
        Category = category;
        CompletationDate = completationDate;
    }

    public void UpdateGoal(string newGoal)
    {
        ValidateGoal(newGoal);
        Goal = newGoal;
    }
    public static TodoTask LoadFromDb(string id, Guid userId, string goal, string notes, Categories category, DateTime completationDate, bool done)
    {
        var task = new TodoTask(userId, goal, notes, category, completationDate)
        {
            Id = id
        };

        if (done)
            task.MarkAsDone();
        
        return task;
    }

    public void UpdateNotes(string newNotes)
    {
        ValidateNotes(newNotes);
        Notes = newNotes;
    }

    public void UpdateCategory(Categories newCategory) =>
        Category = newCategory;

    public void UpdateDueDate(DueDate newDate)
    {
        CompletationDate = newDate;  
    }

    public void MarkAsDone()
    {
        Done = true;
        CompletationDate = DateTime.Now;
    }

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