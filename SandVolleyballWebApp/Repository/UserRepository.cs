using Microsoft.EntityFrameworkCore;
using SandVolleyballWebApp.Data;
using SandVolleyballWebApp.Interfaces;
using SandVolleyballWebApp.Models;

namespace SandVolleyballWebApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(AppUser user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(AppUser user)
        {
            _context.Remove(user);
            return Save();
        }

        public async Task<ICollection<AppUser>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }
        public AppUser GetUser(string id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
           _context.Update(user);
            return Save();
        }

        
    }
}
