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
    /**
     * @class AccountsController
     * @brief Controller for user authentication and registration.
     */
    public class AccountsController : Controller
    {
        private readonly IUserService _userService;

        /**
         * @brief Constructor for AccountsController.
         * @param userService Service for handling user authentication and registration.
         */
        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }

        /**
         * @brief Displays the login and registration page.
         * @return View of the sign-in/sign-up page.
         */
        [HttpGet]
        public IActionResult SignInAndUpUsers()
        {
            return View();
        }

        /**
         * @brief Handles user login.
         * @param model LoginViewModel containing user credentials.
         * @return Redirects to the profile page if successful; otherwise, reloads the login page with an error message.
         */
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

        /**
         * @brief Handles user registration.
         * @param model RegisterViewModel containing user details.
         * @return Redirects to the home tour page if successful; otherwise, reloads the registration page with an error message.
         */
        [HttpPost]
        public virtual async Task<IActionResult> Register(RegisterViewModel model)
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

        /**
         * @brief Displays the user profile page.
         * @return View containing user profile details.
         */
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

        /**
         * @brief Logs out the current user.
         * @return Redirects to the sign-in/sign-up page.
         */
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignInAndUpUsers");
        }
    }
}
