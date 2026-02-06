using System.ComponentModel.DataAnnotations;

namespace backend.dtos.student;

public record StudentCreateDto(
    [Required]
    [MinLength(10, ErrorMessage = "Name must be at least 10 characters long.")]
    string Name,
    [Required]
    [MinLength(8, ErrorMessage = "Name must be at least 8 characters long.")]
    string Identifier
    );