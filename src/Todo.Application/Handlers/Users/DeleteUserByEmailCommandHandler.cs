using Todo.Application.Commands.User;
using Todo.Domain.Interfaces.Repositories;

namespace Todo.Application.Handlers.Users;

public class DeleteUserByEmailCommandHandler
{
    private readonly IUserRepository _repository;

    public DeleteUserByEmailCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(DeleteUserCommand command)
    {
        if(string.IsNullOrWhiteSpace(command.Email) && command.Id == null)
            throw new ArgumentNullException("argumentos nulos");

        if(_repository.GetByEmailAsync(command.Email!) == null)
            throw new ArgumentNullException("usuario nao encontrado/existente");

        await _repository.DeleteAsync(command.Id);
    }
}