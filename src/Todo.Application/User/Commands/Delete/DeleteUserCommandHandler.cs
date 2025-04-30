using Todo.Application.Commands.User;
using Todo.Application.Queries;
using Todo.Domain.Interfaces.Repositories;

namespace Todo.Application.Handlers.Users;

public class DeleteUserCommandHandler
{
    private readonly IUserRepository _repository;
    private readonly GetUserQueryHandler _query;

    public DeleteUserCommandHandler(GetUserQueryHandler query,IUserRepository repository)
    {
        _repository = repository;
        _query = query;
    }

    public async Task HandleAsync(DeleteUserCommand command)
    {
        if(string.IsNullOrWhiteSpace(command.Email) && command.Id == null)
            throw new ArgumentNullException("argumentos nulos");
        
        var user = await _query.Handle(new GetUserQuery(id: command.Id, email: command.Email))!;

        if(user == null)
            throw new InvalidOperationException("usuario nao encontrado/existente");

        await _repository.DeleteAsync(user.Id);
    }
}