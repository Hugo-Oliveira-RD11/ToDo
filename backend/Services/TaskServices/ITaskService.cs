using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DTO;
using backend.Models;

namespace backend.Services.TaskServices;

public interface ITaskService
{
    List<TasksUsersDTO?> GetAllTasksByUser(Guid userId);
    TasksUsersDTO? GetTaskById(string id);
    List<TasksUsersDTO?> GetAllTasksToday(Guid userId);
    Task<TasksUsersDTO?> CreateAsync(Guid userId, TasksUsers newTask);
    Task<TasksUsersDTO?> UpdateAsync(string id, TasksUsersDTO updatedTask);
    Task<bool> RemoveAsync(string id);
}