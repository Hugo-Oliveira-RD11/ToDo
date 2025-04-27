using MongoDB.Driver;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces.Repositories;
using Todo.Infrastructure.Data.Model;
using Todo.Infrastructure.Mapping;

namespace Todo.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly IMongoCollection<TodoTaskModel> _taskCollection;

    public TaskRepository(IMongoDatabase database)
    {
        _taskCollection = database.GetCollection<TodoTaskModel>("Tasks");
    }

    public async Task<TodoTask> GetTaskByIdAsync(string id)
    {
        var taskModel = await _taskCollection
            .Find(t => t.Id == id)
            .FirstOrDefaultAsync();

        return taskModel != null ? TaskMapping.ToTask(taskModel) : null;
    }

    public async Task<TodoTask?> GetTaskByGoalAsync(string goal)
    {
        var taskModel = await _taskCollection
            .Find(t => t.Goal == goal)
            .FirstOrDefaultAsync();

        return taskModel != null ? TaskMapping.ToTask(taskModel) : null;
    }

    public async Task<IEnumerable<TodoTask>> GetAllAsync()
    {
        var taskModels = await _taskCollection
            .Find(t => true)
            .ToListAsync();

        return taskModels.Select(TaskMapping.ToTask);
    }

    public async Task AddAsync(TodoTask todoTask)
    {
        var taskModel = TaskMapping.ToTaskModel(todoTask);
        await _taskCollection.InsertOneAsync(taskModel);
    }

    public async Task UpdateAsync(TodoTask todoTask)
    {
        var taskModel = TaskMapping.ToTaskModel(todoTask);
        var filter = Builders<TodoTaskModel>.Filter.Eq(t => t.Id, todoTask.Id);
        var result = await _taskCollection.ReplaceOneAsync(filter, taskModel);

        if (result.MatchedCount == 0)
            throw new Exception("Tarefa não encontrada para atualização.");
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<TodoTaskModel>.Filter.Eq(t => t.Id, id);
        await _taskCollection.DeleteOneAsync(filter);
    }

}