using backend.dtos.correction;

namespace backend.dtos.student;

public record StudentDto(
    int Id,
    string Name,
    string Identifier,
    List<CorrectionMiniDto> Corrections);