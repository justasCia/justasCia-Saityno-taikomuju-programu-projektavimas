using System.ComponentModel.DataAnnotations;

namespace BallTalkAPI.Auth.DTOs
{
    public class RegisterUserDto
    {
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
