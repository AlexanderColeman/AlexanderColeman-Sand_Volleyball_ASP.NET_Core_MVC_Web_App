using SandVolleyballWebApp.Models;
using System.Collections.Generic;

namespace SandVolleyballWebApp.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<AppUser>> GetAllUsers();
        Task<AppUser> GetUserByIdAsync(string id);
        AppUser GetUser(string id);
        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(AppUser user);
        bool Save();
        

    }
}
