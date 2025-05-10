using Todo.Domain.Entities;

namespace Todo.Domain.Interfaces.Repositories;
public interface ITodoTaskRepository
{
    Task<IEnumerable<TodoTask?>> GetByUserIdPagedAsync(Guid userId, int pageNumber, int pageSize);
    Task<TodoTask?> GetTaskByIdAndUserIdAsync(string id, Guid userId);
    Task<TodoTask?> GetTaskByGoalAndUserIdAsync(string goal, Guid userId);
    Task<IEnumerable<TodoTask>> GetAllAsync(Guid userId);
    Task AddAsync(TodoTask todoTask);
    Task UpdateAsync(TodoTask todoTask);
    Task DeleteAsync(string id);
}