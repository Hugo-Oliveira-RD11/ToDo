using backend.DTO;
using backend.Models;
using backend.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("v1/user")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    public UserController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
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

#endif
}