﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;
using SiteRBC.Models.SignInAndUpUsers;
using System.Data.SqlClient;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> Login(LoginViewModel model)
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

            // Створення Claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.FullName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim("UserId", user.Id.ToString()) // Зберігаємо Id користувача
    };

            // Створення ClaimsIdentity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Аутентифікація користувача
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            TempData["Success"] = "Login successful!";
            return RedirectToAction("Profile"); // Переходимо на сторінку профілю
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
