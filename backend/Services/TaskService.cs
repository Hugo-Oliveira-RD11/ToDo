using backend.DTO;
using backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace backend.Services;

public class TaskService
{
    private readonly IMongoCollection<TasksUsers> _tasksUsers;

    private readonly UserService _userService;

    public TaskService(
        IOptions<TasksUsersDatabaseSettings> tasksDatabase, UserService userServices
        )
    {
        _userService = userServices;
        var mongoConnection = new MongoClient(tasksDatabase.Value.ConnectionString);
        var mongoDatabase = mongoConnection.GetDatabase(tasksDatabase.Value.DatabaseName);
        _tasksUsers = mongoDatabase.GetCollection<TasksUsers>(tasksDatabase.Value.DatabaseName);
    }

    public List<TasksUsersDTO> GetAllTasksByUser(Guid userId)
    {

        var listTask = _tasksUsers.AsQueryable()
            .Where(t => t.UserId == userId)
            .Select(t => new TasksUsersDTO
            {
                Id = t.Id,
                Feito = t.Feito,
                Notas = t.Notas,
                Objetivo = t.Objetivo,
                ADayToComplet = t.ADayToComplet,
                Category = t.Category
            })
            .OrderBy(t => t.Objetivo)
            .ThenBy(t => t.Feito)
            .ToList();

        return listTask;
    }

    public TasksUsersDTO GetTaskById(string id)
    {
        var listTask = _tasksUsers.AsQueryable().Where(t => t.Id == id).Select(t => new TasksUsersDTO
        {
            Id = t.Id,
            Feito = t.Feito,
            Notas = t.Notas,
            Objetivo = t.Objetivo,
            ADayToComplet = t.ADayToComplet,
            Category = t.Category
        }).FirstOrDefault();

        return listTask;

    }
    public List<TasksUsersDTO> GetAllTasksToday(Guid userId)
    {
        var listTask = _tasksUsers.AsQueryable()
            .Where(t => t.UserId == userId && t.ADayToComplet == DateTime.Today)
            .Select(t => new TasksUsersDTO
            {
                Id = t.Id,
                Feito = t.Feito,
                Notas = t.Notas,
                Objetivo = t.Objetivo,
                ADayToComplet = t.ADayToComplet,
                Category = t.Category
            })
            .OrderBy(t => t.Objetivo)
            .ThenBy(t => t.Feito)
            .ToList();

        return listTask;
    }

    public async Task<TasksUsersDTO?> CreateAsync(TasksUsers newTask)
    {
        if(_userService.GetUserById(newTask.UserId) == null)
            return null;

        await  _tasksUsers.InsertOneAsync(newTask);
        var task = new TasksUsersDTO
        {
                Id = newTask.Id,
                Feito = newTask.Feito,
                Notas = newTask.Notas,
                Objetivo = newTask.Objetivo,
                ADayToComplet = newTask.ADayToComplet,
                Category = newTask.Category
        };

        return task;
    }

    public async Task<TasksUsersDTO?> UpdateAsync(string id, TasksUsers updatedTask)  
    {
        if(_userService.GetUserById(updatedTask.UserId) == null)
            return null;

        await _tasksUsers.ReplaceOneAsync(x => x.Id == id, updatedTask);

        var task = new TasksUsersDTO
        {
                Id = updatedTask.Id,
                Feito = updatedTask.Feito,
                Notas = updatedTask.Notas,
                Objetivo = updatedTask.Objetivo,
                ADayToComplet = updatedTask.ADayToComplet,
                Category = updatedTask.Category
        };

        return task;
    }
    public async Task RemoveAsync(string id) => // nao vejo o que retornar
        await _tasksUsers.DeleteOneAsync(x => x.Id == id);
}