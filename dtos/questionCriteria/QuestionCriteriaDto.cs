namespace backend.dtos.questionCriteria;

using backend.enums;

public record QuestionCriteriaDto(
    int Id,
    CritereaEnum Type,
    string Criteria
    );