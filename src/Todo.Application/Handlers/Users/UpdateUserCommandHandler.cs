using Todo.Application.Commands.User;
using Todo.Domain.Interfaces.Repositories;

namespace Todo.Application.Handlers.Users;

public class UpdateUserCommandHandler
{
    private readonly IUserRepository _repository;

    public UpdateUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UpdateUserCommand command)
    {
        var existingUser = await _repository.GetByIdAsync(command.Id);

        if (existingUser == null)
            throw new ArgumentNullException("Usuario nao existente");

        if(!string.IsNullOrWhiteSpace(command.Name))
            existingUser.UpdateName(command.Name);

        if(!string.IsNullOrWhiteSpace(command.Email))
            existingUser.UpdateEmail(command.Email);

        if(!string.IsNullOrWhiteSpace(command.Password))
            existingUser.UpdatePassword(command.Password);

        await _repository.UpdateAsync(existingUser);
    }
}