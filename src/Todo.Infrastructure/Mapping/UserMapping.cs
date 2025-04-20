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
            Email = user.Email.ToString(),
            Password = user.Password
        };
    }

    public static User ToUser(UserModel userModel)
    {
        return new User(
            id: userModel.Id,
            name: userModel.Name,
            email: new Email(userModel.Email),
            password: userModel.Password
        );
    }
}