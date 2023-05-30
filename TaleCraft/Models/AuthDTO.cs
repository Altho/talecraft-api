using System.ComponentModel.DataAnnotations;

namespace TaleCraft.Models;

public class UserForRegisterDto
{
    [Required] 
    public string Username { get; set; }
 
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters.")]
    public string Password { get; set; }
    public string Role { get; set; }
}

public class UserForLoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}