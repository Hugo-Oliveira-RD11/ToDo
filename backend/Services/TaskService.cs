using backend.DTO;
using backend.Models;

using Microsoft.Extensions.Options;

using MongoDB.Driver;

using TasksUsersDTO = backend.DTO.TasksUsersDTO;

namespace backend.Services;

public class TaskService
{
    private readonly IMongoCollection<TasksUsers> _tasksUsers;
    public TaskService(
        IOptions<TasksUsersDatabaseSettings> tasksDatabase
        )
    {
        var mongoConnection = new MongoClient(tasksDatabase.Value.ConnectionString);
        var mongoDatabase = mongoConnection.GetDatabase(tasksDatabase.Value.DatabaseName);
        _tasksUsers = mongoDatabase.GetCollection<TasksUsers>(tasksDatabase.Value.DatabaseName);
    }

    public async Task<List<TasksUsersDTO>> GetAllTasksByUser(Guid userId)
    {

        var tasksDTO =_tasksUsers.AsQueryable().Where(t => t.UserId == userId).Select(t => new TasksUsersDTO
        {
            Id = t.Id,
            Feito = t.Feito,
            Notas = t.Notas,
            Objetivo = t.Objetivo,
            ADayToComplet = t.ADayToComplet,
            Category = t.Category
        }).ToList();

        return tasksDTO;
    }

    public TasksUsersDTO GetTaskByUser(string id) {
        var taskDTO = _tasksUsers.AsQueryable().Where(t => t.Id == id).Select(t => new TasksUsersDTO{
            Id = t.Id,
            Feito = t.Feito,
            Notas = t.Notas,
            Objetivo = t.Objetivo,
            ADayToComplet = t.ADayToComplet,
            Category = t.Category
            }).FirstOrDefault();

        return taskDTO;
        
    }

    public async Task CreateAsync(TasksUsers newTask) =>
        await _tasksUsers.InsertOneAsync(newTask);

    public async Task UpdateAsync(string id, TasksUsers updatedTask) =>
        await _tasksUsers.ReplaceOneAsync(x => x.Id == id, updatedTask);

    public async Task RemoveAsync(string id) =>
        await _tasksUsers.DeleteOneAsync(x => x.Id == id);
}