using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Entities;
using Todo.Application.Users.DTOs;
using Todo.Application.Users.Mapping;
using Todo.Domain.Shared;

namespace Todo.Application.Users.Queries.GetUser;
public class GetUserHandler
{
    private readonly IUserRepository _userRepository;

    public GetUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDTO?>> Handle(GetUserQuery query)
    {
        User? user = null;
        if(query.Id != null)
            user = await _userRepository.GetByIdAsync(query.Id.Value);
        
        if (user == null && !string.IsNullOrWhiteSpace(query.Email))
            user = await _userRepository.GetByEmailAsync(query.Email);
        
        if(user == null)
            return Result<UserDTO>.Fail("usuario nao achado");
        
        UserDTO userDto = UserMappingDTO.ToUserDto(user);
        return Result<UserDTO?>.Ok(userDto);
    }

}