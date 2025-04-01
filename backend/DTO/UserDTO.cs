namespace backend.DTO;

public class UserDTO{
    public Guid? Id {get; set;} 
    public string? Name {get; set;} 
    public string? Email { get; set; } 
    public UserDTO(){
        this.Id = null;
        this.Name = null;
        this.Email = null;
    }
}