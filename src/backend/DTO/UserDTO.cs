namespace backend.DTO;

public record UserDTO(
    Guid? Id,
    string? Name,
    string? Email
);