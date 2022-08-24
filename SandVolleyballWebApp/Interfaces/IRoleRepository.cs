using Microsoft.AspNetCore.Identity;
using SandVolleyballWebApp.Models;

namespace SandVolleyballWebApp.Interfaces
{
    public interface IRoleRepository
    {
        IdentityRole GetRole(string id);
        Task<IdentityRole> GetRoleAsync(string id);
        int GetRolesCount(string id);
        ICollection<IdentityRole> GetRoles();
    }
}
