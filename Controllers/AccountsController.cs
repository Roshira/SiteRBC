using Microsoft.AspNetCore.Mvc;
using SiteRBC.Models.SignInAndUpUsers;

namespace SiteRBC.Controllers
{
    public class AccountsController : Controller
    {
        [HttpGet]
        public IActionResult SignInAndUpUsers()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
           
            if (!ModelState.IsValid)
            {
                // Якщо дані некоректні, повертаємо користувача на ту ж сторінку з помилками.
                TempData["Error"] = "Invalid login details.";
                return RedirectToAction("SignInAndUpUsers");
            }

            // Перевірка логіну (простий приклад; реальна логіка може включати доступ до БД).
            if (model.Email == "user@example.com" && model.Password == "password123")
            {
                TempData["Success"] = "Login successful!";
                return RedirectToAction("AdminMenu", "AdminFunctional"); // Перенаправлення на іншу сторінку.
            }

            TempData["Error"] = $"{model.Email},{model.Password}";
            return RedirectToAction("SignInAndUpUsers");
        }

        // POST: Register
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please correct the errors in the form.";
                return RedirectToAction("SignInAndUpUsers");
            }

            // Збереження користувача (можливо, в базі даних).
            TempData["Success"] = "Registration successful! You can now log in.";
            return RedirectToAction("SignInAndUpUsers");
        }


    }
}
