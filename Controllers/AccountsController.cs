using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;
using SiteRBC.Models.SignInAndUpUsers;
using System.Data.SqlClient;
using BCrypt.Net;

namespace SiteRBC.Controllers
{
    public class AccountsController : Controller
    {

        private readonly SiteRBCContext _context;

        public AccountsController(SiteRBCContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult SignInAndUpUsers()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill in all fields correctly.";
                return RedirectToAction("SignInAndUpUsers");
            }

            // Знаходимо користувача за email
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                TempData["Error"] = "Invalid email or password.";
                return RedirectToAction("SignInAndUpUsers");
            }

            TempData["Success"] = "Login successful!";
            return RedirectToAction("Tour", "Home");
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please correct the errors in the form.";
                return RedirectToAction("SignInAndUpUsers");
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            if (existingUser != null)
            {
                TempData["Error"] = "Email is already registered.";
                return RedirectToAction("SignInAndUpUsers");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new Users
            {
                FullName = model.FullName,
                Email = model.Email,
                Password = hashedPassword
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "Registration successful! You can now log in.";
            return RedirectToAction("Tour", "Home");
        }


    }
}
