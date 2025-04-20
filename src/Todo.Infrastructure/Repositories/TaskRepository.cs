using MongoDB.Driver;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces.Repositories;
using Todo.Infrastructure.Data.Model;
using Todo.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Infrastructure.Data.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly IMongoCollection<TaskModel> _taskCollection;

    public TaskRepository(IMongoDatabase database)
    {
        _taskCollection = database.GetCollection<TaskModel>("Tasks");
    }

    public async Task<Task> GetByIdAsync(string id)
    {
        var taskModel = await _taskCollection
            .Find(t => t.Id == id)
            .FirstOrDefaultAsync();

        return taskModel != null ? TaskMapping.ToTask(taskModel) : null;
    }

    public async Task<IEnumerable<Task>> GetAllAsync()
    {
        var taskModels = await _taskCollection
            .Find(t => true)
            .ToListAsync();

        return taskModels.Select(TaskMapping.ToTask);
    }

    public async Task AddAsync(Task task)
    {
        var taskModel = TaskMapping.ToTaskModel(task);
        await _taskCollection.InsertOneAsync(taskModel);
    }

    public async Task UpdateAsync(Task task)
    {
        var taskModel = TaskMapping.ToTaskModel(task);
        var filter = Builders<TaskModel>.Filter.Eq(t => t.Id, task.Id);
        var result = await _taskCollection.ReplaceOneAsync(filter, taskModel);

        if (result.MatchedCount == 0)
            throw new Exception("Tarefa não encontrada para atualização.");
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<TaskModel>.Filter.Eq(t => t.Id, id);
        await _taskCollection.DeleteOneAsync(filter);
    }

}