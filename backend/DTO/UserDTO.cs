namespace backend.DTO;

public class UserDTO{
    public Guid? Id {get; set;} = null;
    public string? Name {get; set;} = string.Empty;
    public string? Email { get; set; } = string.Empty;
}