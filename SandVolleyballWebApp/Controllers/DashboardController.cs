using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SandVolleyballWebApp.Data;
using SandVolleyballWebApp.Interfaces;
using SandVolleyballWebApp.Models;
using SandVolleyballWebApp.ViewModels;

namespace SandVolleyballWebApp.Controllers
{
    public class DashboardController : Controller
    { 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        private void MapUserEdit(AppUser user, EditUserDashboardViewModel editVM, ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.ProfileImageUrl = photoResult.Url.ToString();
            user.Bio = editVM.Bio;
            user.City = editVM.City;
            user.State = editVM.State;
            user.PlayStyleGroup = editVM.PlayStyleGroup;

        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userCourts = await _unitOfWork.Dashboard.GetAllUserCourts();
            var dashboardViewModel = new DashboardViewModel()
            {
                Courts = userCourts
            };
            return View(dashboardViewModel);
        }

        [Authorize]
        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _unitOfWork.Dashboard.GetUserById(curUserId);
            if (user == null) return View("Error");
            var editUserViewModel = new EditUserDashboardViewModel()
            {
                Id = curUserId,
                ProfileImageUrl = user.ProfileImageUrl,
                Bio = user.Bio,
                City = user.City,
                State = user.State,
                PlayStyleGroup = user.PlayStyleGroup
            };
            return View(editUserViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserProfilePost(EditUserDashboardViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editVM);
            }

            var user = await _unitOfWork.Dashboard.GetByIdNoTracking(editVM.Id);

            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _unitOfWork.Photo.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);

                _unitOfWork.Dashboard.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _unitOfWork.Photo.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editVM);
                }
                var photoResult = await _unitOfWork.Photo.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);

                _unitOfWork.Dashboard.Update(user);

                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AdminEditUser(string id)
        {
            var user = await _unitOfWork.Dashboard.GetUserById(id);
            if (user == null) return View("Error");
            var editUserViewModel = new EditUserDashboardViewModel()
            {
                Id = id,
                ProfileImageUrl = user.ProfileImageUrl,
                Bio = user.Bio,
                City = user.City,
                State = user.State,
                PlayStyleGroup = user.PlayStyleGroup,
            };
            return View(editUserViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminEditUserPost(string id, EditUserDashboardViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("AdminEditUser", editVM);
            }

            var user = await _unitOfWork.Dashboard.GetByIdNoTracking(id);

            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _unitOfWork.Photo.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);

                _unitOfWork.Dashboard.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _unitOfWork.Photo.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editVM);
                }
                var photoResult = await _unitOfWork.Photo.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);

                _unitOfWork.Dashboard.Update(user);

                return RedirectToAction("Index", "User");
            }
        }

    }
}
