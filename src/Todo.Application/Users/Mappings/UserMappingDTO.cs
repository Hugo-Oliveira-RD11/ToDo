using System.Security.Cryptography;
using Todo.Application.Users.DTOs;
using Todo.Domain.Entities;
    
namespace Todo.Application.Users.Mapping;

public static class UserMappingDTO
{
    public static UserDTO ToUserDto(User user)
    {
        return new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
    
    public static User ToUser(UserDTO userDTO , string password = null)
    {
        var passwordHash = string.IsNullOrWhiteSpace(password) ? null : password; // caminhando pro futuro com hash de senha
        var user = User.LoadFromDb(
            id: userDTO.Id,
            name: userDTO.Name,
            email: userDTO.Email,
            password: passwordHash 
        );
            
        return user;
    }

}