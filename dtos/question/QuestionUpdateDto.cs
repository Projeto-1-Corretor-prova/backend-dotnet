using System.ComponentModel.DataAnnotations;

namespace backend.dtos.question;

public record QuestionUpdateDto(
    [Required]
    [MinLength(15, ErrorMessage = "Statement must be at least 15 characters long.")]
    string Statement
    );