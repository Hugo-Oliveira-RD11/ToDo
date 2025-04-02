using backend.DTO;
using backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace backend.Services;

public class TaskService
{
    private readonly IMongoCollection<TasksUsers> _tasksUsers;
    private readonly IServiceScopeFactory _scopeFactory;

    public TaskService(IOptions<TasksUsersDatabaseSettings> tasksDatabase, IServiceScopeFactory scopeFactory)
    {
        if (tasksDatabase?.Value?.ConnectionString == null)
            throw new ArgumentNullException(nameof(tasksDatabase.Value.ConnectionString), "MongoDB connection string is null.");

        if (tasksDatabase?.Value?.DatabaseName == null)
            throw new ArgumentNullException(nameof(tasksDatabase.Value.DatabaseName), "MongoDB database name is null.");

        _scopeFactory = scopeFactory;

        var mongoConnection = new MongoClient(tasksDatabase.Value.ConnectionString);
        var mongoDatabase = mongoConnection.GetDatabase(tasksDatabase.Value.DatabaseName);
        _tasksUsers = mongoDatabase.GetCollection<TasksUsers>(tasksDatabase.Value.CollectionName);
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

    public async Task<TasksUsersDTO?> CreateAsync(Guid userId, TasksUsers newTask)
    {
        if (newTask == null)
            throw new ArgumentNullException(nameof(newTask), "A tarefa não pode ser nula.");

        if (userId == Guid.Empty)
            throw new ArgumentException("UserId inválido.", nameof(userId));

        using var scope = _scopeFactory.CreateScope();
        var userContext = scope.ServiceProvider.GetRequiredService<UserService>();

        if (userContext.GetUserById(userId) == null)
            return null;

        newTask.Id ??= ObjectId.GenerateNewId().ToString();
        newTask.UserId = userId;
        newTask.ADayToComplet ??= DateTime.UtcNow;

        await _tasksUsers.InsertOneAsync(newTask);

        return new TasksUsersDTO
        {
            Id = newTask.Id,
            Feito = newTask.Feito,
            Notas = newTask.Notas,
            Objetivo = newTask.Objetivo,
            ADayToComplet = newTask.ADayToComplet,
            Category = newTask.Category
        };
    }

    public async Task<TasksUsersDTO?> UpdateAsync(string id, TasksUsers updatedTask)
    {
        using var scope = _scopeFactory.CreateScope();
        var userContext = scope.ServiceProvider.GetRequiredService<UserService>();
        if (userContext.GetUserById(updatedTask.UserId) == null)
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
    public async Task<bool> RemoveAsync(string id)
    {
        try
        {
            await _tasksUsers.DeleteOneAsync(x => x.Id == id);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"error: {e}");
            return false;
        }
    }
}