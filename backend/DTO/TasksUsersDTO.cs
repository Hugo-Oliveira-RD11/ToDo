using backend.Models;

namespace backend.DTO;

public class TasksUsersDTO
{
    public string Id { get; set; } = string.Empty;
    public string Objetivo { get; set; } = string.Empty;
    public string Notas { get; set; } = string.Empty;
    public bool Feito { get; set; } = false;
    public Category Category { get; set; } = Category.White;
    public DateTime? ADayToComplet { get;set; } = null;
}
