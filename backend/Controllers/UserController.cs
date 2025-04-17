using backend.DTO;
using backend.Models;
using backend.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


#if DEBUG
using System.Diagnostics;
using backend.Data;
#endif

namespace backend.Controllers;

[Route("v1/user")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
#if DEBUG
    private readonly UserContext _context;
    private readonly IPasswordService _passwordService;
#endif

    public UserController(IUserService userService
#if DEBUG
                          , UserContext context
                          , IPasswordService passwordService
#endif
    )
    {
        _userService = userService;
        #if DEBUG
        _context = context;
        _passwordService = passwordService;
        #endif
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<UserDTO>> CreateUser([FromBody] User newUser)
    {
        var userDTO = await _userService.CreateUser(newUser);
        return Ok(userDTO);
    }

    [HttpDelete]
    public ActionResult DeleteUserById([FromQuery] Guid deleteUser)
    {
        try
        {
            bool complet = _userService.DeleteUserById(deleteUser);
            return Ok(complet);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"internal error: {e}");
        }
    }

    [HttpPatch]
    public async Task<ActionResult<UserDTO>> UpdateUser([FromQuery] Guid userId, [FromBody] User updatedUser)
    {
        UserDTO? response = await _userService.UpdateUserById(userId, updatedUser);

        if (response == null)
            return StatusCode(404, "user dont exist");

        return Ok(response);
    }


#if DEBUG
    [HttpGet]
    [AllowAnonymous] //nao quero ter que logar para ver os usuario
    public async Task<ActionResult<List<UserDTO>>> GetAllUsersDebug(int numberPage=1,int sizePage=100)
    {
        var users = await _userService.GetAllUsers(numberPage, sizePage);
        return Ok(users);
    }

    [HttpGet("{userID}")]
    [AllowAnonymous] 
    public ActionResult<UserDTO> GetUserDebug(Guid userID)
    {
        UserDTO? users = _userService.GetUserDTOById(userID);
        return Ok(users);
    }

    [HttpGet("test-insert")]
    [AllowAnonymous]
    public async Task<IActionResult> TestInsert()
    {
        var sw = new Stopwatch();
        sw.Start();

        _context.Users.Add(new User
        {
            Name = "Test",
            Email = Guid.NewGuid().ToString("N") + "@mail.com",
            Password = "123456"
        });

        await _context.SaveChangesAsync();

        sw.Stop();
        return Ok($"Tempo de inserção: {sw.ElapsedMilliseconds}ms");
    }

    [HttpGet("test-password")]
    [AllowAnonymous]
    public async Task<IActionResult>  TestTimerPassword()
    {
        var sw = Stopwatch.StartNew();


        var cost = _passwordService.Cost();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword("12345", cost);

        sw.Stop();
        return Ok($"Tempo para gerar senha: {sw.ElapsedMilliseconds}ms\n usando {cost} de custo");
    }
#endif
}