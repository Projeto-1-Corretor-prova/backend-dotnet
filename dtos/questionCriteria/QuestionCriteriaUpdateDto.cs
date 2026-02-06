using System.ComponentModel.DataAnnotations;
using backend.enums;

namespace backend.dtos.questionCriteria;

public record QuestionCriteriaUpdateDto(
    [Required]
    CritereaEnum Type,
    [Required]
    [MinLength(5, ErrorMessage = "Criteria must be at least 5 characters long.")]
    string Criteria
    );