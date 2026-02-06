using System.ComponentModel.DataAnnotations;

namespace backend.dtos.teacher;

public record LoginDto(
    string Name,
    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    string Password,
    [EmailAddress(ErrorMessage =  "Invalid Email Address")]
    string Email
    );