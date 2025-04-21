using Todo.Domain.Entities;

namespace Todo.Domain.Interfaces.Repositories;
public interface ITaskRepository
{
    Task<TodoTask> GetTaskByIdAsync(string id);
    Task<IEnumerable<TodoTask>> GetAllAsync();
    Task AddAsync(TodoTask todoTask);
    Task UpdateAsync(TodoTask todoTask);
    Task DeleteAsync(string id);
}