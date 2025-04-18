using Todo.Domain.Entities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Todo.Domain.Interfaces.Repositories;
public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync(); 
    Task AddAsync(User user); 
    Task UpdateAsync(User user); 
    Task DeleteAsync(Guid id); 
    Task<User> GetByEmailAsync(string email); 
}