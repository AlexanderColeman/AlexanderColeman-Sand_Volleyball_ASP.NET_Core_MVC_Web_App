using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using SandVolleyballWebApp.Models;

namespace SandVolleyballWebApp.ViewModels
{
    public class EditRoleViewModel
    {
        public AppUser User { get; set; }
        public IList<SelectListItem> Roles { get; set; }

    }
}
