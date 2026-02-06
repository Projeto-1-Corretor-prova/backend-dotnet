using System.ComponentModel.DataAnnotations;

namespace backend.dtos.answer;

public record AnswerUpdateDto(
    string AnswerStudent,
    [Required]
    [Range(0, 100, ErrorMessage = "Score must be between 0 and 100.")]
    double Score
    );