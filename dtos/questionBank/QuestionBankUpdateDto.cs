using System.ComponentModel.DataAnnotations;

namespace backend.dtos.questionBank;

public record QuestionBankUpdateDto(
    [Required]
    [MinLength(3, ErrorMessage = "Class name must be at least 3 characters long.")]
    string Title);