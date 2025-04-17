using backend.DTO;
using backend.Models;
using backend.Services.AuthServices;
using backend.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("v1/user/login")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenService _refreshToken;
    public AuthController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpGet("/login")]
    public ActionResult Login([FromBody] LoginDTO loginUser)
    {
        if(loginUser == null)
            return StatusCode(400, "values equal null");

        var user = _userService.GetUserByEmail(loginUser.Email!) ?? null;

        if(user == null)
            return StatusCode(404, "user dont exist");

        var token = _tokenService.Generate(user);
        var accessToken = GuidService.GuidGenerate();
        Response.Cookies.Append(
        "refreshToken",
        accessToken.ToString(),
        new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });

        _refreshToken.SetAsync(user.Id.ToString(), accessToken.ToString());
        
        return Ok(new {jwt = token, longToken = accessToken});
    }
}