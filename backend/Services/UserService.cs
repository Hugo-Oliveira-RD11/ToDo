using backend.Data;
using backend.DTO;
using backend.Models;

using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class UserServices
{
    private readonly UserContext _context;

    public UserServices(UserContext context)
    {
        _context = context;
    }

    public async Task<UserDTO> CreateUser(User user)
    {
        user.Password = PasswordService.PasswordGenerate(user.Password);
        user.Id = GuidService.GuidGenerate();

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        UserDTO userDTO = new UserDTO
        {
            Name = user.Name,
            Email = user.Email,
            Id = user.Id
        };

        return userDTO;
    }

    public User? GetUserById(Guid userSearch) =>
        _context.Users.FirstOrDefault(user => user.Id == userSearch);

    public async Task<UserDTO> Update(Guid userId, User newUser)
    {
        
        UserDTO userDTO = new UserDTO
        {
            Id = null,
            Name = null,
            Email = null
        };
        User user = GetUserById(userId);
        if (user == null)
            return userDTO;

        _context.Users.Where(u => u == newUser).ExecuteUpdate(null);
        await _context.SaveChangesAsync();

        return userDTO;
    }

    public async Task<bool> DeleteUser(Guid userId) 
    {
        User user = GetUserById(userId);

        _context.Users.Where(n => n == user).ExecuteDelete();
        await _context.SaveChangesAsync();
        if (user == null)
            return false;

        return true;
    }

}