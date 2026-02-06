using backend.dtos.correction;
using backend.dtos.questionTestWritten;

namespace backend.dtos.testWritten;

public record TestWrittenDto(
    int Id,
    string Title,
    string RegexQuestionIdentifier,
    double Weight,
    List<QuestionTestWrittenDto> Questions,
    List<CorrectionMiniDto> Corrections
    );