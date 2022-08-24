using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SandVolleyballWebApp.Models;

namespace SandVolleyballWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<ExperienceGroup> ExperienceGroups { get; set; }

    }
}

