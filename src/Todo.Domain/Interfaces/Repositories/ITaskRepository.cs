using Todo.Domain.Entities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Todo.Domain.Interfaces.Repositories;
public interface ITaskRepository
{
    Task<Task> GetTaskByIdAsync(string id);
    Task<IEnumerable<Task>> GetAllAsync();
    Task AddAsync(Task task);
    Task UpdateAsync(Task task);
    Task DeleteAsync(string id);
}