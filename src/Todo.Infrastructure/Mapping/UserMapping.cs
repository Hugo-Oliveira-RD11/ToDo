using Todo.Domain.Entities;
using Todo.Infrastructure.Data.Model;

namespace Todo.Infrastructure.Mapping;
public static class UserMapping
{
    public static UserModel ToUserModel(User user)
    {
        return new UserModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password
        };
    }

    public static User ToUser(UserModel userModel)
    {
        return new User
        {
            Id = userModel.Id,
            Name = userModel.Name,
            Email = userModel.Email,
            Password = userModel.Password
        };
    }
}