using backend.dtos.correction;
using backend.dtos.questionTestWritten;

namespace backend.dtos.testWritten;

public record TestWrittenDto(
    int Id,
    string Title,
    string RegexQuestionIdentifier,
    double TotalWeight,
    List<QuestionTestWrittenDto> QuestionTestWrittens,
    List<CorrectionMiniDto> Corrections
    );