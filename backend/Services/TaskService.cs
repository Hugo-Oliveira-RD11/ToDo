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

    public List<TasksUsersDTO?> GetAllTasksByUser(Guid userId)
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
            .OrderBy(t => t.Feito)
            .ThenBy(t => t.Objetivo)
            .ToList();

        return listTask;
    }

    private TasksUsers? GetRealTaskById(string id)
    {
        var listTask = _tasksUsers.AsQueryable()
            .Where(t => t.Id == id)
            .FirstOrDefault();

        return listTask;

    }

    public TasksUsersDTO? GetTaskById(string id)
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

    public List<TasksUsersDTO?> GetAllTasksToday(Guid userId)
    {
        var listTask = _tasksUsers.AsQueryable()
            .Where(t => t.UserId == userId && t.ADayToComplet >= DateTime.Today && t.ADayToComplet < DateTime.Today.AddDays(1))
            .Select(t => new TasksUsersDTO
            {
                Id = t.Id,
                Feito = t.Feito,
                Notas = t.Notas,
                Objetivo = t.Objetivo,
                ADayToComplet = t.ADayToComplet,
                Category = t.Category
            })
            .OrderBy(t => t.Feito)
            .ThenBy(t => t.Objetivo)
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

    public async Task<TasksUsersDTO?> UpdateAsync(string id, TasksUsersDTO updatedTask)
    {
        var oldTask = GetRealTaskById(id);
        if(oldTask == null) 
            return null;

        using var scope = _scopeFactory.CreateScope();
        var userContext = scope.ServiceProvider.GetRequiredService<UserService>();

        if (userContext.GetUserById(oldTask.UserId) == null)
            return null;
    
        var updateDefinition = Builders<TasksUsers>.Update
            .Set(t => t.Objetivo, updatedTask.Objetivo)
            .Set(t => t.Notas, updatedTask.Notas)
            .Set(t => t.Feito, updatedTask.Feito)
            .Set(t => t.ADayToComplet, updatedTask.ADayToComplet)
            .Set(t => t.Category, updatedTask.Category);

        await _tasksUsers.UpdateOneAsync(x => x.Id == id, updateDefinition);

        var task = new TasksUsersDTO
        {
            Id = id,
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
            if(GetTaskById(id) == null)
                return false;

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