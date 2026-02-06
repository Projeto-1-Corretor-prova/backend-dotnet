using System.ComponentModel.DataAnnotations;

namespace backend.dtos.testWritten;

public record TestWrittenUpdateDto(
    [Required]
    [MinLength(3, ErrorMessage = "Title must be at least 3 characters long.")]
    string Title,
    [Required]
    string RegexQuestionIdentifier);