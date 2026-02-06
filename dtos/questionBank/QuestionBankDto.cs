using backend.dtos.question;

namespace backend.dtos.questionBank;

public record QuestionBankDto(
    int Id,
    string Title,
    List<QuestionMiniDto> Questions);