using System.ComponentModel.DataAnnotations;

using Todo.Domain.Entities;

namespace Todo.Infrastructure.Data.Model;
public class UserModel
{
    [Key]
    public Guid Id { get; set; }
    
    [StringLength(250, MinimumLength = 3)]
    public string Name { get; set; }
    
    [DataType(DataType.EmailAddress)]
    [Required]
    public string Email { get; set; } 
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } 
}