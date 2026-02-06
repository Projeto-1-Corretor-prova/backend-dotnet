using backend.dtos.answer;
using backend.dtos.student;
using backend.dtos.testWritten;

namespace backend.dtos.correction;

public record CorrectionMiniDto(
    int Id,
    double Score,
    TestWrittenMiniDto? TestWritten,
    StudentMiniDto? Student);