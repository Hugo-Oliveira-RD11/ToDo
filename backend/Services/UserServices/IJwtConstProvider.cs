using Microsoft.IdentityModel.Tokens;

namespace backend.Services.UserServices;

public interface IJwtConstProvider
{
    SymmetricSecurityKey GetKey();
    string GetConfig(string config);
}