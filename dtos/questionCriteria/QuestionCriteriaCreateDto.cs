using System.ComponentModel.DataAnnotations;

namespace backend.dtos.questionCriteria;

using backend.enums;

public record QuestionCriteriaCreateDto(
    [Required]
    CritereaEnum Type,
    [Required]
    [MinLength(5, ErrorMessage = "Criteria must be at least 5 characters long.")]
    string Criteria
    );