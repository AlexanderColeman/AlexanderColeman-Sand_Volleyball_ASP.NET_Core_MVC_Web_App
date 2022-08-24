using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SandVolleyballWebApp.Interfaces;
using SandVolleyballWebApp.Models;
using SandVolleyballWebApp.ViewModels;

namespace SandVolleyballWebApp.Controllers
{
    public class UserController : Controller
    { 
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(IUnitOfWork unitOfWork, SignInManager<AppUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }

        [Authorize]
        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _unitOfWork.User.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    ProfileImageUrl = user.ProfileImageUrl,

                };
                result.Add(userViewModel);

            }
            return View(result);
        }

        [Authorize]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _unitOfWork.User.GetUserByIdAsync(id);

            var profile = new UserProfileInformation
            {
                Id = user.Id,
                ProfileImageUrl = user.ProfileImageUrl,
                Bio = user.Bio,
                City = user.City,
                State = user.State,
                UserName = user.UserName,
                PlayStyleGroup = user.PlayStyleGroup,
            };

            return View(profile);
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _unitOfWork.User.GetUserByIdAsync(id);
            if (user == null) return View("Error");
            return View(user);

        }
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _unitOfWork.User.GetUserByIdAsync(id);
            if (user == null) return View("Error");

            _unitOfWork.User.Delete(user);
            return RedirectToAction("Index");


        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditRole()
        {
            var users = await _unitOfWork.User.GetAllUsers();
            return View(users);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = _unitOfWork.User.GetUser(id);
            var roles = _unitOfWork.Role.GetRoles();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            var roleItems = roles.Select(role =>
                    new SelectListItem(
                        role.Name,
                        role.Id,
                        userRoles.Any(x => x.Contains(role.Name)))).ToList();

            var vm = new EditRoleViewModel
            {
                User = user,
                Roles = roleItems
            };
            return View(vm);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(EditRoleViewModel editRoleVm)
        {
            var user = _unitOfWork.User.GetUser(editRoleVm.User.Id);

            if (user == null)
            {
                return NotFound();
            }

            var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);

            var RolesToAdd = new List<string>();
            var RolesToDelete = new List<string>();

            foreach (var role in editRoleVm.Roles)
            {
                var assignInDb = userRolesInDb.FirstOrDefault(x => x == role.Text);
                if (role.Selected)
                {
                    if (assignInDb == null)
                    {
                        //Add Role
                        RolesToAdd.Add(role.Text);
                    }
                }
                else
                {
                    if (assignInDb != null)
                    {
                        //Remove Role
                        RolesToDelete.Add(role.Text);
                    }
                }
            }

            if (RolesToAdd.Any())
            {
                await _signInManager.UserManager.AddToRolesAsync(user, RolesToAdd);
            }

            if (RolesToDelete.Any())
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, RolesToDelete);
            }

            user.UserName = editRoleVm.User.UserName;
            user.Email = editRoleVm.User.Email;

            _unitOfWork.User.Update(user);

            return RedirectToAction("Edit", new {id = user.Id});
        }

    }
}
