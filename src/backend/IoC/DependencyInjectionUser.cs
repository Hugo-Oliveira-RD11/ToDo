using System.IdentityModel.Tokens.Jwt;
using backend.Services.AuthServices;
using backend.Services.UserServices;
using Microsoft.Extensions.DependencyInjection;

namespace backend.IoC;
public static class  DependencyInjectionUser
{
    public static IServiceCollection AddUserServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<JwtSecurityTokenHandler>();
        services.AddScoped<IJwtConstProvider, JwtConstProvider>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        return services;
    }
}