
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
    public TasksUsersDTO GetTaskById(string userId) =>
         _taskService.GetTaskById(userId);

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Guid userId, [FromBody] TasksUsers task) // work
    {
        if (task == null)
            return BadRequest("A tarefa não pode ser nula/ou o json estar errado!!!");

        var createdTask = await _taskService.CreateAsync(userId, task);
        return createdTask != null ? Ok(createdTask) : BadRequest("Erro ao criar a tarefa.");
    }

    [HttpPatch]
    public async Task<TasksUsersDTO?> UpdateAsync(string id, TasksUsers updatedTask) =>
            await _taskService.UpdateAsync(id, updatedTask);

    [HttpDelete]
    public async Task<bool> DeleteAsync(string id)
    {
        bool response = await _taskService.RemoveAsync(id);
        return response;
    }
}