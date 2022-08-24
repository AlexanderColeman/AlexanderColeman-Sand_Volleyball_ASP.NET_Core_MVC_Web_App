using SandVolleyballWebApp.Models;

namespace SandVolleyballWebApp.Interfaces
{
    public interface ICourtRepository
    {
        Task<IEnumerable<Court>> GetAll();
        Task<Court> GetByIdAsync(int id);
        Task<Court> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Court>> GetCourtByCity(string city);
        Task<IEnumerable<Court>> GetCourtByState(string state);
        bool Add(Court court);
        bool Update(Court court);
        bool Delete(Court court);
        bool Save(); 
    }
}
