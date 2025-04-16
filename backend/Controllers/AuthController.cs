using backend.DTO;
using backend.Models;
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
    public AuthController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpGet("/login")]
    public ActionResult<string> Login(LoginDTO loginUser)
    {
        if(loginUser == null)
            return StatusCode(400, "values equal null");

        var user = _userService.GetUserByEmail(loginUser.Email!) ?? null;

        if(user == null)
            return StatusCode(404, "user dont exist");

        return _tokenService.Generate(user);
    }
}