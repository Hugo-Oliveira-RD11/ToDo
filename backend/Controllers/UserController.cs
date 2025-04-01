using backend.DTO;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller;

[Route("v1/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> CreateUser(User newUser)
    {
        var userDTO = _userService.CreateUser(newUser);
        return Ok(userDTO);
    }
    [HttpDelete]
    public async Task<ActionResult> DeleteUserById(Guid deleteUser)
    {
        var userDTO = _userService.GetUserById(deleteUser);
        if(userDTO == null)
            return NotFound(userDTO);

        try{
            bool complet = await _userService.DeleteUserById(deleteUser);
            return Ok();
        }catch(Exception e){
            return StatusCode(500, $"internal error: {e}");
        }
    }

    [HttpPatch]
    public async Task<ActionResult<UserDTO>> UpdateUser(Guid userId, User updatedUser){
        var response =  _userService.UpdateUserById(userId, updatedUser);
        if(response == null)
            return StatusCode(404, "user dont exist");
        return Ok();
    }

    // #if DEBUG
    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> DebugAllUsers() =>
        await _userService.GetAllUsers();
    // #endif
}