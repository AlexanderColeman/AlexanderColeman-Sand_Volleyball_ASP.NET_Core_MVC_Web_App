using Microsoft.AspNetCore.Identity;
using SandVolleyballWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace SandVolleyballWebApp.Models
{
    public class AppUser : IdentityUser
    {
        public PlayStyleGroup? PlayStyleGroup { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public Address? Address { get; set; } 
        public string? Bio { get; set; }
        public int? Age { get; set; }
    }

    public class AppRole : IdentityRole
    {

    }
}
