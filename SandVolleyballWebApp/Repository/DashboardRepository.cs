﻿using Microsoft.EntityFrameworkCore;
using SandVolleyballWebApp.Data;
using SandVolleyballWebApp.Interfaces;
using SandVolleyballWebApp.Models;

namespace SandVolleyballWebApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<List<Court>> GetAllUserCourts()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userCourt = _context.Courts.Where(r => r.AppUser.Id == curUser);
            return userCourt.ToList();
        }
        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
