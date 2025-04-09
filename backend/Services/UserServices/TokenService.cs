using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services.UserServices;


public class TokenService : ITokenService
{
    private readonly IJwtConstProvider _constProvider;
    private readonly JwtSecurityTokenHandler _handler;
    public TokenService(IJwtConstProvider constProvider, JwtSecurityTokenHandler handler)
    {
        _constProvider = constProvider;
        _handler = handler;
    }
    public string Generate(User user)
    {
        var credentials = new SigningCredentials(_constProvider.GetKey(), SecurityAlgorithms.HmacSha512); // refactor
        var claimsUser = GetClaims(user);
        var expiration = DateTime.UtcNow.AddHours(int.Parse(_constProvider.GetConfig("ExpireHours")));

        var token = new JwtSecurityToken(
            issuer: _constProvider.GetConfig("Issuer"),
            audience: _constProvider.GetConfig("Audience"),
            claims: claimsUser,
            expires: expiration,
            signingCredentials: credentials
        );

        return _handler.WriteToken(token);
    }

    private IEnumerable<Claim> GetClaims(User user)
    {
        return new[]{
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email)
        };
    }


}