using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Tourist.API.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? FullName { get; set; }

        [Required]
        public DateTime BirthDay { get; set; }
    }
}