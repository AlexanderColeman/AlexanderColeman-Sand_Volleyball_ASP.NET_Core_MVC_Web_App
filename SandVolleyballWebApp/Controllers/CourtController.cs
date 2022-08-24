using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SandVolleyballWebApp.Data;
using SandVolleyballWebApp.Interfaces;
using SandVolleyballWebApp.Models;
using SandVolleyballWebApp.Repository;
using SandVolleyballWebApp.Services;
using SandVolleyballWebApp.ViewModels;

namespace SandVolleyballWebApp.Controllers
{
    public class CourtController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        

        public CourtController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var courts = await _unitOfWork.Court.GetAll();
            return View(courts);
        }
        [Authorize]
        public async Task<IActionResult> Search(string state)
		{
			var courts = await _unitOfWork.Court.GetCourtByState(state);
			return View(courts);
		}
        [Authorize]
		public async Task<IActionResult> Detail(int id)
        {
            Court court = await _unitOfWork.Court.GetByIdAsync(id);
            return View(court);
        }
        [Authorize]
        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createVeiwModel = new CreateCourtViewModel { AppUserId = curUserId };
            return View(createVeiwModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourtViewModel courtVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _unitOfWork.Photo.AddPhotoAsync(courtVM.Image);

                var court = new Court
                {
                    Title = courtVM.Title,
                    Description = courtVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = courtVM.AppUserId,
                    Address = new Address
                    {
                        Street = courtVM.Address.Street,
                        City = courtVM.Address.City,
                        State = courtVM.Address.State,
                    }
                };
                _unitOfWork.Court.Add(court);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(courtVM);
        }
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var court = await _unitOfWork.Court.GetByIdAsync(id);
            if (court == null) return View("Error");
            var courtVM = new EditCourtViewModel
            {
                Title = court.Title,
                Description = court.Description,
                AddressId = court.Address.Id,
                Address = court.Address,
                URL = court.Image

            };
            return View(courtVM);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditCourtViewModel courtVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", courtVM);
            }
            var userCourt = await _unitOfWork.Court.GetByIdAsyncNoTracking(id);

            if (userCourt != null)
            {
                try
                {
                    await _unitOfWork.Photo.DeletePhotoAsync(userCourt.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(courtVM);
                }
                var photoResult = await _unitOfWork.Photo.AddPhotoAsync(courtVM.Image);

                var court = new Court
                {
                    Id = id,
                    Title = courtVM.Title,
                    Description = courtVM.Description,
                    Image = photoResult.Url.ToString(),
                    Address = courtVM.Address
                };
                _unitOfWork.Court.Update(court);
                return RedirectToAction("Index");
            }
            else
            {
                return View(courtVM);
            }
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var courtDetails = await _unitOfWork.Court.GetByIdAsync(id);
            if (courtDetails == null) return View("Error");
            return View(courtDetails);
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCourt(int id)
        {
            var courtDetails = await _unitOfWork.Court.GetByIdAsync(id);
            if (courtDetails == null) return View("Error");

            _unitOfWork.Court.Delete(courtDetails);
            return RedirectToAction("Index");
        }

    }

}

