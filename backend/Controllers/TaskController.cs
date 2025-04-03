
using backend.DTO;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("v1/task")]
public class TaskController : ControllerBase
{
    private readonly TaskService _taskService;
    public TaskController(TaskService taskService)
    {
        _taskService = taskService;
    }
    [HttpGet]
    public List<TasksUsersDTO> GetTaskAllByUserId([FromQuery] Guid userId) => // work
         _taskService.GetAllTasksByUser(userId);

    [HttpGet("{userId}")]
    public TasksUsersDTO? GetTaskById(string userId) => // work
         _taskService.GetTaskById(userId);

    [HttpGet("today/{userId}")]
    public ActionResult<List<TasksUsersDTO?>> GetAllTasksTodayByUserId(Guid userId) => 
         _taskService.GetAllTasksToday(userId);

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Guid userId, [FromBody] TasksUsers task) // work
    {
        if (task == null)
            return BadRequest("A tarefa não pode ser nula/ou o json estar errado!!!");

        var createdTask = await _taskService.CreateAsync(userId, task);
        return createdTask != null ? Ok(createdTask) : BadRequest("Erro ao criar a tarefa.");
    }

    [HttpPatch]
    public async Task<ActionResult<TasksUsersDTO?>> UpdateAsync( string id, [FromBody] TasksUsersDTO updatedTask) { // work
        if( updatedTask == null)
            return null;

        var response = await _taskService.UpdateAsync(id, updatedTask);
            return Ok(response);
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteAsync([FromQuery] string id) //work
    {
        bool response = await _taskService.RemoveAsync(id);
        return response;
    }
}