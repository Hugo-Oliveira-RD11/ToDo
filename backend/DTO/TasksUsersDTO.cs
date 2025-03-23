namespace backend.DTO;

public class TasksUsersDTO
{
    public Guid UserId { get; set; }
    public List<TaskModelDTO> Tasks { get; set; } = new List<TaskModelDTO>();
}

public class TaskModelDTO
{
    public string Id { get; set; } = string.Empty;
    public string Objetivo { get; set; } = string.Empty;
    public string Notas { get; set; } = string.Empty;
    public bool Feito { get; set; } = false;
}