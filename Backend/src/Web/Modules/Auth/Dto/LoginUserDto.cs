using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Serenity.Modules.Auth.Dto;

public class LoginUserDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}

public class LoginUserResponse
{
    public string Token { get; set; }
    public bool Success { get; set; }
    public List<IdentityError> Errors { get; set; }
}