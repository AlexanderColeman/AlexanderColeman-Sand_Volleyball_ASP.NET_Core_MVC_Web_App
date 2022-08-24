using Microsoft.EntityFrameworkCore;
using SandVolleyballWebApp.Data;
using SandVolleyballWebApp.Interfaces;
using SandVolleyballWebApp.Models;

namespace SandVolleyballWebApp.Repository
{
    public class CourtRepository : ICourtRepository
    {
        private readonly ApplicationDbContext _context;

        public CourtRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Court court)
        {
            _context.Add(court);
            return Save();
        }

        public bool Delete(Court court)
        {
            _context.Remove(court);
            return Save();
        }

        public async Task<IEnumerable<Court>> GetAll()
        {
           return await _context.Courts.ToListAsync();
        }

        public async Task<Court> GetByIdAsync(int id)
        {
            return await _context.Courts.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Court> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Courts.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Court>> GetCourtByCity(string city)
        {
            return await _context.Courts.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Court court)
        {
           _context.Update(court);
            return Save();
        }

        public async Task<IEnumerable<Court>> GetCourtByState(string state)
        {
            return await _context.Courts.Where(c => c.Address.State.Contains(state)).ToListAsync();
        }
    }
}
