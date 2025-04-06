namespace backend.Services.UserServices;

public class PasswordService : IPasswordService
{
    private readonly int _cost;

    public PasswordService(IConfiguration configuration)
    {
        _cost = int.Parse(configuration["BCrypt:Cost"] ?? "12");
    }

    public string PasswordGenerate(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password, workFactor: _cost);

    public bool CheckPasswordCorrect(string password, string hashPassword) =>
        BCrypt.Net.BCrypt.Verify(password, hashPassword);
}