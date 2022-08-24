using Microsoft.AspNetCore.Identity;

namespace SandVolleyballWebApp.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public ICollection<IdentityRole> Roles { get; set; }
        public IdentityRole Role { get; set; }
        public string Name { get; set; }

    }
}
