namespace backend.Services.UserServices;

public interface IPasswordService
{
    string PasswordGenerate(string password);
    bool CheckPasswordCorrect(string password, string hashPassword);
}