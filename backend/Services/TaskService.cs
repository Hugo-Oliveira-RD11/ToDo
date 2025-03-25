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

    public async Task<List<TasksUsersDTO>> GetAsync()
    {
        var queryableCollection = _tasksUsers.AsQueryable();
        var tasksDTO = await from task in queryableCollection select  new TasksUsersDTO()
        {
            Id = task.Id,
            Objetivo = task.Objetivo,
            Notas = task.Notas,
            Feito = task.Feito,
            Category = task.Category,
            ADayToComplet = task.ADayToComplet
        };
        return tasksDTO;
    }

    public async Task<TasksUsers?> GetAsync(string id) =>
        await _tasksUsers.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TasksUsers newTask) =>
        await _tasksUsers.InsertOneAsync(newTask);

    public async Task UpdateAsync(string id, TasksUsers updatedTask) =>
        await _tasksUsers.ReplaceOneAsync(x => x.Id == id, updatedTask);

    public async Task RemoveAsync(string id) =>
        await _tasksUsers.DeleteOneAsync(x => x.Id == id);
}