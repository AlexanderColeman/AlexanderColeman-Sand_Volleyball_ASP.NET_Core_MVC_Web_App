using SandVolleyballWebApp.Models;

namespace SandVolleyballWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Court>> GetAllUserCourts();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
