using Todo.Application.DTOs;
using Todo.Application.Commands.User;
using Todo.Application.Mapping;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Entities;
using Todo.Application.Queries;

namespace Todo.Application.Handlers.Users;

public class CreateUserCommandHandler
{
    private readonly IUserRepository _repository;
    private readonly GetUserQueryHandler _query;

    public CreateUserCommandHandler(IUserRepository repository, GetUserQueryHandler query)
    {
        _repository = repository;
        _query = query;
    }

    public async Task<UserDTO> HandleAsync(CreateUserCommand command)
    {
        var existingUser = await _query.Handle(new GetUserQuery(email: command.Email));

        if (existingUser != null)
            throw new Exception("Já existe um usuário com esse e-mail.");

        var user = new User(command.Name, command.Email, command.Password);
        await _repository.AddAsync(user);
        return UserMappingDTO.ToUserDto(user);
    }
}