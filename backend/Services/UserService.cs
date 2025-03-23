using backend.Data;
using backend.DTO;
using backend.Models;

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

    public UserDTO Update(Guid userId, User newUser)
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


        return userDTO;
    }

    public bool DeleteUser(Guid userId) // refatorar no futuro
    {
        User user = GetUserById(userId);

        if (user == null)
            return false;

        return true;
    }

}