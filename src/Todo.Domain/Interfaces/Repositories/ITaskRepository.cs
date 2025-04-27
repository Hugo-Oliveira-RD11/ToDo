using Todo.Domain.Entities;

namespace Todo.Domain.Interfaces.Repositories;
public interface ITaskRepository
{
    Task<IEnumerable<TodoTask?>> GetByUserIdPagedAsync(Guid userId, int pageNumber, int pageSize);
    Task<TodoTask?> GetTaskByIdAsync(string id);
    Task<TodoTask?> GetTaskByGoalAsync(string goal);
    Task<IEnumerable<TodoTask>> GetAllAsync();
    Task AddAsync(TodoTask todoTask);
    Task UpdateAsync(TodoTask todoTask);
    Task DeleteAsync(string id);
}