using System.ComponentModel.DataAnnotations;

namespace backend.dtos.questionTestWritten;

public record QuestionTestWrittenCreateDto(
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Weight must be a non-negative value.")]
    double Weight,
    [Required]
    [Range(1, 100, ErrorMessage = "Lines shouldn't be above 100 or below 1.")]
    int Lines
);
