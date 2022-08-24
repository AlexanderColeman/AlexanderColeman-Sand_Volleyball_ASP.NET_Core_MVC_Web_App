using Microsoft.AspNetCore.Identity;
using SandVolleyballWebApp.Models;

namespace SandVolleyballWebApp.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        IDashboardRepository Dashboard { get; }
        ICourtRepository Court { get; }
        IPhotoService Photo { get; }
        

    }
}
