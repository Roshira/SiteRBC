using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;
using SiteRBC.Models.SignInAndUpUsers;
using BCrypt.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace SiteRBC.Services
{
    public class AccountService
    {
        private readonly SiteRBCContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Конструктор, що викликає основний з null для IHttpContextAccessor
        public AccountService(SiteRBCContext context)
            : this(context, null)
        {
        }

        // Основний конструктор
        public AccountService(SiteRBCContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // Реєстрація нового користувача
        public async Task<(bool Success, string Message)> RegisterAsync(RegisterViewModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                return (false, "Invalid registration data.");
            }

            bool emailExists = await _context.Users.AnyAsync(u => u.Email == model.Email);
            bool nameExists = await _context.Users.AnyAsync(u => u.FullName == model.FullName);

            if (emailExists || nameExists)
            {
                return (false, "Email or name already registered.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new Users
            {
                FullName = model.FullName,
                Email = model.Email,
                Password = hashedPassword,
                Role = model.Role ?? "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return (true, "Registration successful! You can now log in.");
        }

        // Вхід користувача
        public async Task<(bool Success, string Message)> LoginAsync(LoginViewModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return (false, "Please fill in all fields correctly.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                return (false, "Invalid email or password.");
            }

            // Перевірка, чи доступний HttpContext
            if (_httpContextAccessor?.HttpContext == null)
            {
                return (false, "Authentication service is unavailable.");
            }

            // Створення claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("UserId", user.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return (true, "Login successful");
        }
    }
}
