using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SandVolleyballWebApp.Data;
using SandVolleyballWebApp.Interfaces;
using SandVolleyballWebApp.Models;
using SandVolleyballWebApp.Repository;
using SandVolleyballWebApp.ViewModels;

namespace SandVolleyballWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            if(user != null)
            {
                //User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    // password correct sign in
                    var result = await _signInManager.PasswordSignInAsync(user,loginVM.Password,false,false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Court");
                    }
                }
                //Password is incorrect
                TempData["Error"] = "Wrong credentials. Please, try again";
                return View(loginVM);
            }
            // User not found
            TempData["Error"] = "Wrong credentials. Please try again";
            return View(loginVM);
     
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerVM, ImageUploadResult photoResult)
        {
            
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            //Check to see if app user email exists if so displays error message
            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if(user != null)
            {
                TempData["Error"] = "This email is already in use";
                return View(registerVM);
            }
            var addPhotoResult = await _unitOfWork.Photo.AddPhotoAsync(registerVM.Image);

            var newUser = new AppUser()
            {
                Email = registerVM.EmailAddress,
                UserName = registerVM.UserName,
                ProfileImageUrl = addPhotoResult.Url.ToString()
             };
            var newUserResponse = await _userManager.CreateAsync(newUser,registerVM.Password);

			if (newUserResponse.Succeeded)
			{
				await _userManager.AddToRoleAsync(newUser, UserRoles.User);
			}

            //Added this
            var newLogIn = new LoginViewModel
            {
                EmailAddress = registerVM.EmailAddress,
                Password = registerVM.Password,

            };

           await Login(newLogIn);
            //To this

           return RedirectToAction("Index", "Court");

		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

      
        public IActionResult Profile()
        {

            ViewBag.userId = _userManager.GetUserId(HttpContext.User);

            AppUser user = _userManager.FindByIdAsync(ViewBag.userId).Result;

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
    }
}
