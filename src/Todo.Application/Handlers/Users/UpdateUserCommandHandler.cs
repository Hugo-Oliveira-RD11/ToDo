using Todo.Application.Commands.User;
using Todo.Application.Queries;
using Todo.Domain.Interfaces.Repositories;

namespace Todo.Application.Handlers.Users;

public class UpdateUserCommandHandler
{
    private readonly IUserRepository _repository;
    private readonly GetUserQueryHandler _query;

    public UpdateUserCommandHandler(IUserRepository repository, GetUserQueryHandler query)
    {
        _repository = repository;
        _query = query;
    }

    public async Task HandleAsync(UpdateUserCommand command)
    {
        var existingUser = await _query.Handle(new GetUserQuery(id: command.Id));

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