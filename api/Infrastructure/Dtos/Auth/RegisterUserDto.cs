using System.ComponentModel.DataAnnotations;

namespace api.Infrastructure.Dtos.Auth
{
    public class RegisterUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? MiddleName { get; set; }
        [Required]
        [MaxLength(200)]
        public string FirstSurname { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? SecondSurname { get; set; }

    }
}