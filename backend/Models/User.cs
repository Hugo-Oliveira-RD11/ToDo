using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class User
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Este email nao e valido")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}