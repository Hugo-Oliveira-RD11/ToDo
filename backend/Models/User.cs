using System.ComponentModel.DataAnnotations;
using backend.Services.UserServices;

namespace backend.Models;

public class User
{
    [Required]
    public Guid Id { get; init;} = GuidService.GuidGenerate();

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "Este email nao e valido")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public User(){} // teste para o EF
}