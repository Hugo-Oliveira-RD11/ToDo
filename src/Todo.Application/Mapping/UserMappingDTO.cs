    using System.Security.Cryptography;

    using Todo.Application.DTOs;
    using Todo.Domain.Entities;
    
namespace Todo.Application.Mapping;

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
    
        public static User ToUser(UserDTO userDTO , string password)
        {
            var user = User.LoadFromDb(
                id: userDTO.Id,
                name: userDTO.Name,
                email: userDTO.Email,
                password: password
                );
            
            return user;
        }

}