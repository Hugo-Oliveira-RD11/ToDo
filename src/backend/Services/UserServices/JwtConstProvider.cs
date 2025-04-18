using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services.UserServices;

public class JwtConstProvider : IJwtConstProvider
{
    private readonly IConfiguration _config;
    public JwtConstProvider(IConfiguration config)
    {
        _config = config;
    }
    public SymmetricSecurityKey GetKey()
    {
        var key = _config["JWT:key"] ?? throw new ArgumentNullException("Jwt:key nao foi achado");
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }
    public string GetConfig(string config)
    {
        var key = _config["JWT:" + config] ?? throw new ArgumentNullException($"Jwt:{config} nao foi achado");
        return key;
    }
}