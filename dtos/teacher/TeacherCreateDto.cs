using System.ComponentModel.DataAnnotations;

namespace backend.dtos.teacher;

public record TeacherCreateDto(
    [Required]
    [MinLength(6, ErrorMessage = "Name must be at least 6 characters long.")]
    string Name,
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    string Email,
    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    string Password
    );