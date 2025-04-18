namespace Todo.Domain.Entities;

public class User
{
    public Guid Id { get; init;} 
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private  set; } = string.Empty;

    public User(){} // teste para o EF

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

    private void ValidateEmail(string email)
    {
        if(string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("O e-mail não pode estar vazio.");

         string pattern = @"^((?!\.)[\w\-_\.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$";

         if(!Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
            throw new ArgumentException("O e-mail informado invalido.");
    }
    private void ValidatePassword(string password)
    {
        var number = 8;
        if(string.IsNullOrEmpty(password) || senha.Length < number)
            throw new ArgumentException($"A senha deve ter pelo menos {number} caracteres");
    }

    private void ValidateName(string name)
    {
        var number = 3;
        if(string.IsNullOrEmpty(password) || senha.Length < number)
            throw new ArgumentException($"A senha deve ter pelo menos {number} caracteres");
    }

    public void LimparSenha()
    {
        this.Password = null;
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrEmpty(newName))
            throw new ArgumentException("O nome não pode ser vazio.");

        Nome = newName;
    }

    public void UpdateEmail(string newEmail)
    {
        ValidarEmail(newEmail);
        Email = newEmail;
    }

    public void UpdatePassword(string newPassword)
    {
        ValidarSenha(newPassword);
        Senha = newPassword;  
    }
}