using backend.dtos.questionCriteria;

namespace backend.dtos.question;

public record QuestionDto(
    int Id,
    string Statement,
    List<QuestionCriteriaDto> QuestionCriterias
    );