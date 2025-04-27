using backend.DTO;
using backend.Models;
using backend.Services.AuthServices;
using backend.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("v1/user/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenService _refreshToken;

    public AuthController(IUserService userService, ITokenService tokenService, IRefreshTokenService refreshToken)
    {
        _userService = userService;
        _tokenService = tokenService;
        _refreshToken = refreshToken;
    }

    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginDTO loginUser)
    {
        if(loginUser == null)
            return StatusCode(400, "values equal null");

        var user = _userService.GetUserByEmail(loginUser.Email!) ?? null;

        if(user == null)
            return StatusCode(404, "user dont exist");

        var token = _tokenService.Generate(user);
        var accessToken = GuidService.GuidGenerate();

        _refreshToken.SetAsync(accessToken.ToString(), user.Id.ToString());

        MakeCookie(accessToken);

        return Ok(new {jwt = token, longToken = accessToken});
    }

    [HttpPost("refresh")]
    public async Task<ActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if(string.IsNullOrEmpty(refreshToken))
            return Unauthorized("token nao encontrado no cookie");

        var userId = await _refreshToken.GetAsync(refreshToken.ToString());
        if(string.IsNullOrEmpty(userId))
            return Unauthorized("token invalido ou expirado");

        var user = _userService.GetUserById(Guid.Parse(userId));
        if(user == null)
            return NotFound("Usuario nao existe ou nao encontrado");

        var newJwt = _tokenService.Generate(user);
        var newRefreshToken = GuidService.GuidGenerate();

        await _refreshToken.SetAsync(newRefreshToken.ToString(), user.Id.ToString());

        MakeCookie(newRefreshToken);
        return Ok(new { jwt = newJwt });
    }

    private void MakeCookie(Guid accessToken)
    {
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
    }
}