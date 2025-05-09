using Todo.Application.Users.Commands.Delete;
using Todo.Application.Users.DTOs;
using Todo.Application.Users.Mapping;
using Todo.Application.Users.Queries.GetUser;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Shared;

namespace Todo.Application.Users.Commands.Update;

public class UpdateUserHandler
{
    private readonly IUserRepository _repository;
    private readonly GetUserHandler _query;

    public UpdateUserHandler(IUserRepository repository, GetUserHandler query)
    {
        _repository = repository;
        _query = query;
    }

    public async Task<Result<UserDTO>> HandleAsync(UpdateUserCommand command)
    {
        Result<UserDTO> result = await _query.Handle(new GetUserQuery(id: command.Id));

        User existingUser = UserMappingDTO.ToUser(result.Data); 
        
        if (existingUser == null)
            throw new ArgumentNullException("Usuario nao existente");

        if(!string.IsNullOrWhiteSpace(command.Name))
            existingUser.UpdateName(command.Name);

        if(!string.IsNullOrWhiteSpace(command.Email))
            existingUser.UpdateEmail(command.Email);

        if(!string.IsNullOrWhiteSpace(command.Password))
            existingUser.UpdatePassword(command.Password);

        await _repository.UpdateAsync(existingUser);
        
        var updateUser = UserMappingDTO.ToUserDto(existingUser);
        return Result<UserDTO>.Ok(updateUser);
    }
}