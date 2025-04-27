using Todo.Domain.Entities;
using Todo.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Todo.Domain.Interfaces.Repositories;
using Todo.Infrastructure.Data;
using Todo.Infrastructure.Mapping;

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

    #if DEBUG
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var userModel = await _context.Users.ToListAsync();
        return userModel.Select(UserMapping.ToUser);
    }
    #endif

    public async Task AddAsync(User user)
    {
        var userModel = UserMapping.ToUserModel(user);
        await _context.Users.AddAsync(userModel);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        var userModel = UserMapping.ToUserModel(user);
        _context.Attach(userModel);

        if(userModel.Name is not null)
            _context.Entry(userModel).Property(u => u.Name).IsModified = true;

        if(userModel.Email is not null)
            _context.Entry(userModel).Property(u => u.Email).IsModified = true;

        if(userModel.Password is not null)
            _context.Entry(userModel).Property(u => u.Password).IsModified = true;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
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