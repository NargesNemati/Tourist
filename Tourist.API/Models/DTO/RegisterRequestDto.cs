using System.ComponentModel.DataAnnotations;

namespace Tourist.API.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[]? Roles { get; set; }
        [Required]
        public string? FullName { get; set; }

        [Required]
        public DateTime BirthDay { get; set; }

    }
}
