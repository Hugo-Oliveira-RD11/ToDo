namespace Todo.Application.Dtos;

public class TaskDto
{
    public string Id { get; set; } = string.Empty;
    public string Goal { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public Category Category { get; set; } = Category.White;
    public bool Done { get; set; } = false;
    public DateTime? ADayToComplet { get; set; }
}