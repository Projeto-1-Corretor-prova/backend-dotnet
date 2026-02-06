using backend.dtos.question;

namespace backend.dtos.questionTestWritten;

public record QuestionTestWrittenDto(
    int Id,
    double Weight,
    int Lines,
    QuestionMiniDto Question
    );