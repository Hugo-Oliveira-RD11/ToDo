using Todo.Application.Users.DTOs;
using Todo.Application.Users.Queries.GetUser;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Shared;

namespace Todo.Application.Users.Commands.Delete;

public class DeleteUserHandler
{
    private readonly IUserRepository _repository;
    private readonly GetUserHandler _query;

    public DeleteUserHandler(GetUserHandler query,IUserRepository repository)
    {
        _repository = repository;
        _query = query;
    }

    public async Task HandleAsync(DeleteUserCommand command)
    {
        if(string.IsNullOrWhiteSpace(command.Email) && command.Id == null)
            throw new ArgumentNullException("argumentos nulos");
        
        var result = await _query.Handle(new GetUserQuery(id: command.Id, email: command.Email))!;

        if(result == null)
            throw new InvalidOperationException("usuario nao encontrado/existente");

        await _repository.DeleteAsync(result.Data!.Id);
    }
}