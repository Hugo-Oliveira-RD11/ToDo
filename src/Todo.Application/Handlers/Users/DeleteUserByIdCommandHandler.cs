using Todo.Application.Commands.User;
using Todo.Domain.Interfaces.Repositories;

namespace Todo.Application.Handlers.Users;

public class DeleteUserByIdCommandHandler
{
    private readonly IUserRepository _repository;

    public DeleteUserByIdCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async void HandleAsync(DeleteUserCommand command)
    {
        if(string.IsNullOrWhiteSpace(command.Email) && command.Id == null)
            throw new ArgumentNullException("argumentos nulos");

        if(_repository.GetByIdAsync(command.Id) == null)
            throw new ArgumentNullException("usuario nao encontrado/existente");

        await _repository.DeleteAsync(command.Id);
    }
}