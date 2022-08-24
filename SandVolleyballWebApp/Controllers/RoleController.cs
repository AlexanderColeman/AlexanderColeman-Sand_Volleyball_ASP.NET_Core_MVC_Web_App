using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SandVolleyballWebApp.Data;
using SandVolleyballWebApp.Interfaces;
using SandVolleyballWebApp.Models;
using SandVolleyballWebApp.ViewModels;

namespace SandVolleyballWebApp.Controllers
{
    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public RoleController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManger, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(RoleViewModel roleVm)
        {
            roleVm.Roles = _unitOfWork.Role.GetRoles();
            return View(roleVm.Roles);
        }

        [HttpGet]
        public IActionResult Upsert(string id, RoleViewModel roleVm)
        {
            if (String.IsNullOrEmpty(id))
            {
                return View();
            }
            else
            {
                roleVm.Role = _unitOfWork.Role.GetRole(id);
                return View(roleVm.Role);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(RoleViewModel roleVm)
        {
            if (await _roleManager.RoleExistsAsync(roleVm.Name))
            {
                return RedirectToAction("Index");
            }
            if (string.IsNullOrEmpty(roleVm.Id))
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = roleVm.Name });
                
            }
            else
            {
                var roleDb = await _unitOfWork.Role.GetRoleAsync(roleVm.Id);
                if (roleDb == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                roleDb.Name = roleVm.Name;
                roleDb.NormalizedName = roleVm.Name.ToUpper();
                var result = await _roleManager.UpdateAsync(roleDb);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var roleDb = await _unitOfWork.Role.GetRoleAsync(id);

            if (roleDb == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var userRolesForThisRole = _unitOfWork.Role.GetRolesCount(id);
            if(userRolesForThisRole > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            await _roleManager.DeleteAsync(roleDb);
            return RedirectToAction(nameof(Index)); 

        }
        
    }
}
