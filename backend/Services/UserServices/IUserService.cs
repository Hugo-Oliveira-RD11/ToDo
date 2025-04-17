using backend.DTO;
using backend.Models;

namespace backend.Services.UserServices;

public interface IUserService
{
    Task<UserDTO> CreateUser(User user);
    User? GetUserById(Guid userSearch);
    User? GetUserByEmail(string userSearch);
    Task<UserDTO?> UpdateUserById(Guid userId, User newUser);
    bool DeleteUserById(Guid userId);

#if DEBUG
    UserDTO? GetUserDTOById(Guid userSearch);
    Task<List<UserDTO>> GetAllUsers(int numberPage, int sizePage);
#endif
}