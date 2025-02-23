using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;
using SiteRBC.Models.SignInAndUpUsers;
using System.Data.SqlClient;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SiteRBC.Services.AccountSevice;

namespace SiteRBC.Controllers
{
	public class AccountsController : Controller
	{

        private readonly IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
		public IActionResult SignInAndUpUsers()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill in all fields correctly.";
                return RedirectToAction("SignInAndUpUsers");
            }

            var user = await _userService.AuthenticateUser(model.Email, model.Password);
            if (user == null)
            {
                TempData["Error"] = "Invalid email or password.";
                return RedirectToAction("SignInAndUpUsers");
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.FullName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role),
        new Claim("UserId", user.Id.ToString())
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Profile");
        }

        [HttpPost]
        public virtual async Task<IActionResult> Register(RegisterViewModel model) // Додайте `async` і повертайте `Task<IActionResult>`
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please correct the errors in the form.";
                return RedirectToAction("SignInAndUpUsers");
            }

            if (!await _userService.RegisterUser(model))
            {
                TempData["Error"] = "Email or name already registered.";
                return RedirectToAction("SignInAndUpUsers");
            }

            TempData["Success"] = "Registration successful! You can now log in.";
            return RedirectToAction("Tour", "Home");
        }
        [HttpGet]
		[Authorize]
		public IActionResult Profile()
		{
			var userName = User.Identity.Name;
			var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

			var userModel = new UserProfileViewModel
			{
				FullName = userName,
				Email = userEmail
			};

			return View(userModel);
		}
		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("SignInAndUpUsers");
		}
	}
}
