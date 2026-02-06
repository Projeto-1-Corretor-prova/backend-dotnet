using System.ComponentModel.DataAnnotations;
using backend.dtos.questionCriteria;

namespace backend.dtos.question;

public record QuestionCreateDto(
    [Required]
    [MinLength(15, ErrorMessage = "Statement must be at least 15 characters long.")]
    string Statement,
    List<QuestionCriteriaCreateDto> QuestionCriteriaCreateDtos 
    );