using Todo.Domain.Entities;
using Todo.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Todo.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var userModel = await _context.Users.FindAsync(id);
        return userModel == null ? null : UserMapping.ToUser(userModel);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        var userModel = UserMapping.ToUserModel(user);
        await _context.Users.AddAsync(userModel);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        
        if (user == null)
            throw new NullReferenceException("este usuario nao existe");
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var userModel = await _context.Users
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync();
        
        return userModel == null ? null : UserMapping.ToUser(userModel);
    }
}