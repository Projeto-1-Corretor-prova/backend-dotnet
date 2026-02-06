using System.ComponentModel.DataAnnotations;

namespace backend.dtos.comment;

public record CommentCreateDto(
    [Required]
    [MinLength(10, ErrorMessage = "Content must be at least 10 character long.")]
    string Content
    );