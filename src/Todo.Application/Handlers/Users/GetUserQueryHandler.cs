using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Entities;
using Todo.Application.Queries;

namespace Todo.Application.Handlers.Users;
public class GetUserQueryHandler
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(GetUserQuery query)
    {
        if(query.Id.HasValue)
            return await _userRepository.GetByIdAsync(query.Id.Value!);

        if(!string.IsNullOrWhiteSpace(query.Email))
            return await _userRepository.GetByEmailAsync(query.Email!);

        return null;
    }

}