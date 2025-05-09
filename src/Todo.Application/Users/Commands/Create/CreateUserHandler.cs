using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Entities;
using Todo.Application.Users.DTOs;
using Todo.Application.Users.Mapping;
using Todo.Application.Users.Queries.GetUser;
using Todo.Domain.Shared;

namespace Todo.Application.Users.Commands.Create;

public class CreateUserHandler
{
    private readonly IUserRepository _repository;
    private readonly GetUserHandler _query;

    public CreateUserHandler(IUserRepository repository, GetUserHandler query)
    {
        _repository = repository;
        _query = query;
    }

    public async Task<Result<UserDTO>> HandleAsync(CreateUserCommand command)
    {
        var existingUser = await _query.Handle(new GetUserQuery(email: command.Email));
        if (existingUser != null)
            throw new Exception("Já existe um usuário com esse e-mail."); // mudar no futuro 

        var user = new User(command.Name, command.Email, command.Password);
        await _repository.AddAsync(user);
        UserDTO userDto = UserMappingDTO.ToUserDto(user);
        
        return Result<UserDTO>.Ok(userDto);
    }
}