using backend.Data;
using backend.DTO;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class UserService
{
    private readonly UserContext _context;

    public UserService(UserContext context)
    {
        _context = context;
    }

    public async Task<UserDTO> CreateUser(User user)
    {
        user.Password = PasswordService.PasswordGenerate(user.Password);


        var newUser = new User()
        {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        UserDTO userDTO = new UserDTO
        {
            Name = newUser.Name,
            Email = newUser.Email,
            Id = newUser.Id
        };

        return userDTO;
    }

    public User? GetUserById(Guid userSearch) =>
        _context.Users.FirstOrDefault(user => user.Id == userSearch);

    public async Task<UserDTO?> UpdateUserById(Guid userId, User newUser)
    {
        User? user = GetUserById(userId);
        if (user == null)
            return null;

        newUser.Password = PasswordService.PasswordGenerate(newUser.Password);
        user.Name = newUser.Name ?? user.Name;
        user.Email = newUser.Email ?? user.Email;
        user.Password = newUser.Password ?? user.Password;

        await _context.SaveChangesAsync();

        var userDTO = new UserDTO
        {
            Id = user.Id,
            Name = newUser.Name ?? user.Name,
            Email = newUser.Email ?? user.Email
        };

        return userDTO;
    }

    public bool DeleteUserById(Guid userId) // atualizar no futuro para deletar todas as tasks do usuario!
    {
        User? user = GetUserById(userId);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        _context.SaveChanges();

        return true;
    }

#if DEBUG
    public UserDTO? GetUserDTOById(Guid userSearch) =>
        _context.Users
        .Select(u => new UserDTO
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email
        })
        .FirstOrDefault(user => user.Id == userSearch);

    public async Task<List<UserDTO>> GetAllUsers(int numberPage, int sizePage)
    {
        var usersDTO = await _context.Users
            .AsNoTracking()
            .OrderBy(u => u.Id)
            .Skip((numberPage - 1) * sizePage)
            .Take(sizePage)
            .Select(u => new UserDTO
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            })
            .ToListAsync();
        return usersDTO;
    }
#endif

}