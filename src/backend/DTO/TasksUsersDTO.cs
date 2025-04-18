using backend.Models;

namespace backend.DTO;

public record TasksUsersDTO(
    string? Id,
    string? Objetivo,
    string? Notas,
    bool? Feito,
    Category Category,
    DateTime? ADayToComplet
);