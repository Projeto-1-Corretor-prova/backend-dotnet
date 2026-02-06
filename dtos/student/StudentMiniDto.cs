using backend.dtos.correction;

namespace backend.dtos.student;

public record StudentMiniDto(
    int Id,
    string Name,
    string Identifier);