using backend.Data;
using backend.DTO;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.UserServices;

public class UserService : IUserService
{
    private readonly UserContext _context;
    private readonly IPasswordService _passwordService;

    public UserService(UserContext context, IPasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    public async Task<UserDTO> CreateUser(User user)
    {
        var newUser = new User
        {
            Name = user.Name,
            Email = user.Email,
            Password = _passwordService.PasswordGenerate(user.Password)
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return ToDTO(newUser);
    }

    public User? GetUserById(Guid userId) =>
        _context.Users.FirstOrDefault(user => user.Id == userId);

    public User? GetUserByEmail(string userEmail) =>
        _context.Users.FirstOrDefault(user => user.Email == userEmail);

    public async Task<UserDTO?> UpdateUserById(Guid userId, User updatedUser)
    {
        var existingUser = GetUserById(userId);
        if (existingUser == null)
            return null;

        existingUser.Name = updatedUser.Name ?? existingUser.Name;
        existingUser.Email = updatedUser.Email ?? existingUser.Email;
        existingUser.Password = _passwordService.PasswordGenerate(updatedUser.Password ?? existingUser.Password);

        await _context.SaveChangesAsync();

        return ToDTO(existingUser);
    }

    public bool DeleteUserById(Guid userId)
    {
        var user = GetUserById(userId);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        _context.SaveChanges();

        return true;
    }

#if DEBUG
    public UserDTO? GetUserDTOById(Guid userId) =>
        _context.Users
            .Select(ToDTO)
            .FirstOrDefault(user => user.Id == userId);

    public async Task<List<UserDTO>> GetAllUsers(int pageNumber, int pageSize) =>
        await _context.Users
            .AsNoTracking()
            .OrderBy(u => u.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(u => new UserDTO(u.Id, u.Name, u.Email))
            .ToListAsync();
#endif

    private static UserDTO ToDTO(User user) => new(
        user.Id,
        user.Name,
        user.Email
    );
}