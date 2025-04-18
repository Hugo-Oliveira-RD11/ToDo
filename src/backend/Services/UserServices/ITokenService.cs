using backend.Models;

namespace backend.Services.UserServices;

public interface ITokenService
{
    string Generate(User user);
}