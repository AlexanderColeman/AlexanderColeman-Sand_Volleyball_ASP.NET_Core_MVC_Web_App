using SandVolleyballWebApp.Interfaces;

namespace SandVolleyballWebApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository User { get; }
        public IRoleRepository Role { get; }
        public IDashboardRepository Dashboard { get; }
        public ICourtRepository Court { get; }
        public IPhotoService Photo { get; }
       

    public UnitOfWork(IUserRepository user, IRoleRepository role, IDashboardRepository dashboard, ICourtRepository court, IPhotoService photo)
        {
            User = user;
            Role = role;
            Dashboard = dashboard;
            Court = court;
            Photo = photo;
        }
    }
}
