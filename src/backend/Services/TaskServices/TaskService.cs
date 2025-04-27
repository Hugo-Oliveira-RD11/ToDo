using backend.DTO;
using backend.Models;
using backend.Services.UserServices;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace backend.Services.TaskServices;

public class TaskService : ITaskService
{
    private readonly IMongoCollection<TasksUsers> _tasksUsers;
    private readonly IUserService _userService;

    public TaskService(
        IOptions<TasksUsersDatabaseSettings> tasksDatabase,
        IUserService userService)
    {
        if (tasksDatabase?.Value?.ConnectionString == null)
            throw new ArgumentNullException(nameof(tasksDatabase.Value.ConnectionString), "MongoDB connection string is null.");

        if (tasksDatabase?.Value?.DatabaseName == null)
            throw new ArgumentNullException(nameof(tasksDatabase.Value.DatabaseName), "MongoDB database name is null.");

        _userService = userService;

        var mongoConnection = new MongoClient(tasksDatabase.Value.ConnectionString);
        var mongoDatabase = mongoConnection.GetDatabase(tasksDatabase.Value.DatabaseName);
        _tasksUsers = mongoDatabase.GetCollection<TasksUsers>(tasksDatabase.Value.CollectionName);
    }

    public List<TasksUsersDTO?> GetAllTasksByUser(Guid userId) =>
        _tasksUsers.AsQueryable()
            .Where(t => t.UserId == userId)
            .Select(ToDTO)
            .OrderBy(t => t.Feito)
            .ThenBy(t => t.Objetivo)
            .ToList();

    private TasksUsers? GetRealTaskById(string id) =>
        _tasksUsers.AsQueryable()
            .FirstOrDefault(t => t.Id == id);

    public TasksUsersDTO? GetTaskById(string id) =>
        _tasksUsers.AsQueryable()
            .Where(t => t.Id == id)
            .Select(ToDTO)
            .FirstOrDefault();

    public List<TasksUsersDTO?> GetAllTasksToday(Guid userId) =>
        _tasksUsers.AsQueryable()
            .Where(t => t.UserId == userId &&
                        t.ADayToComplet >= DateTime.Today &&
                        t.ADayToComplet < DateTime.Today.AddDays(1))
            .Select(ToDTO)
            .OrderBy(t => t.Feito)
            .ThenBy(t => t.Objetivo)
            .ToList();

    public async Task<TasksUsersDTO?> CreateAsync(Guid userId, TasksUsers newTask)
    {
        if (newTask == null)
            throw new ArgumentNullException(nameof(newTask), "A tarefa não pode ser nula.");

        if (userId == Guid.Empty)
            throw new ArgumentException("UserId inválido.", nameof(userId));

        if (_userService.GetUserById(userId) == null)
            return null;

        newTask.Id ??= ObjectId.GenerateNewId().ToString();
        newTask.UserId = userId;
        newTask.ADayToComplet ??= DateTime.UtcNow;

        await _tasksUsers.InsertOneAsync(newTask);

        return ToDTO(newTask);
    }

    public async Task<TasksUsersDTO?> UpdateAsync(string id, TasksUsersDTO updatedTask)
    {
        var oldTask = GetRealTaskById(id);
        if (oldTask == null)
            return null;

        if (_userService.GetUserById(oldTask.UserId) == null)
            return null;

        var updateDefinition = Builders<TasksUsers>.Update
            .Set(t => t.Objetivo, updatedTask.Objetivo)
            .Set(t => t.Notas, updatedTask.Notas)
            .Set(t => t.Feito, updatedTask.Feito)
            .Set(t => t.ADayToComplet, updatedTask.ADayToComplet)
            .Set(t => t.Category, updatedTask.Category);

        await _tasksUsers.UpdateOneAsync(x => x.Id == id, updateDefinition);

        return updatedTask with { Id = id };
    }

    public async Task<bool> RemoveAsync(string id)
    {
        try
        {
            if (GetTaskById(id) == null)
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

    private static TasksUsersDTO ToDTO(TasksUsers t) => new(
        t.Id,
        t.Objetivo,
        t.Notas,
        t.Feito,
        t.Category,
        t.ADayToComplet
    );
}