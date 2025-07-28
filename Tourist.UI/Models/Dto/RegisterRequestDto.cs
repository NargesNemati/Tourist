using System.ComponentModel.DataAnnotations;

namespace Tourist.UI.Models.Dto
{
    public class RegisterRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string[]? Roles { get; set; }
        public string? FullName { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
