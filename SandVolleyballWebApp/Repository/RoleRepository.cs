using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SandVolleyballWebApp.Data;
using SandVolleyballWebApp.Interfaces;
using SandVolleyballWebApp.Models;

namespace SandVolleyballWebApp.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IdentityRole GetRole(string id)
        {
            var userRole = _context.Roles.FirstOrDefault(x => x.Id == id);
            return userRole;
        }

        public async Task<IdentityRole> GetRoleAsync(string id)
        {
            var userRole = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            return userRole;
        }

        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public int GetRolesCount(string id)
        {
            var roleCount = _context.UserRoles.Where(x => x.RoleId == id).Count();
            return roleCount;
        }
    }
}
