using Microsoft.AspNetCore.Mvc;

namespace SiteRBC.Controllers
{
    public class AccountsController : Controller
    {
        [HttpGet]
        public IActionResult SignInUsers()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignInUsers(string username, string password)
        {
            if (username == "admin" && password == "password")
            {
                return RedirectToAction("Tour", "Home");
            }
            else
            {
                ViewBag.Error = "You entered an invalid password";
                return View();
            }
        }
    }
}
