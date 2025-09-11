using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOS;

public class RegisterRequest
{
    [Required]
    public required string DisplayName { get; set; }
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MinLength(6)]
    public required string Password { get; set; }

}
