using System.ComponentModel.DataAnnotations;

namespace backend.dtos.comment;

public record CommentUpdateDto(
    [Required]
    [MinLength(10, ErrorMessage = "Content must be at least 10 character long.")]
    string Content
    );