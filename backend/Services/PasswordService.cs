
namespace backend.Services;

public class PasswordService
{
    private static int cost = 12; // tem que mudar, serio

    public static string PasswordGenerate(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password, workFactor: cost);
    public static bool CheckPasswordCorrect(string password,string hashPassword) =>
        BCrypt.Net.BCrypt.Verify(password,hashPassword);
}