using Todo.Application.DTOs;
using Todo.Application.Commands.User;
using Todo.Application.Mapping;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces.Repositories;

namespace Todo.Application.Handlers.Users;

public class CreateUserCommandHandler
{
    private readonly IUserRepository _repository;

    public CreateUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserDTO> HandleAsync(CreateUserCommand command)
    {
        var existingUser = await _repository.GetByEmailAsync(command.Email);
        if (existingUser != null)
            throw new Exception("Já existe um usuário com esse e-mail.");

        var user = new User(command.Name, command.Email, command.Password);

        await _repository.AddAsync(user);

        return UserMappingDTO.ToUserDto(user);
    }
}