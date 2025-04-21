
namespace Todo.Domain.Entities;

public class User
{
    public Guid Id { get; init; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;

    public User() {}

    public User(string name, string email, string password)
    {
        this.Id = Guid.NewGuid();
        ValidateName(name);
        this.Name = name;
        ValidateEmail(email);
        this.Email = email;
        ValidatePassword(password);
        this.Password = password;
    }

    private void ValidatePassword(string password)
    {
        const int minLength = 8;
        if (string.IsNullOrEmpty(password) || password.Length < minLength)
            throw new ArgumentException($"A senha deve ter pelo menos {minLength} caracteres.");
    }

    private void ValidateName(string name)
    {
        const int minLength = 3;
        if (string.IsNullOrEmpty(name) || name.Length < minLength)
            throw new ArgumentException($"O nome deve ter pelo menos {minLength} caracteres.");
    }

    public void LimparSenha()
    {
        this.Password = string.Empty;
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrEmpty(newName))
            throw new ArgumentException("O nome não pode ser vazio.");

        ValidateName(newName);  
        Name = newName;
    }

    public void UpdateEmail(Email newEmail)
    {
        if (newEmail == null)
            throw new ArgumentException("O e-mail não pode ser vazio.");

        Email = newEmail;  
    }

    public void UpdatePassword(string newPassword)
    {
        ValidatePassword(newPassword);
        Password = newPassword;
    }
}