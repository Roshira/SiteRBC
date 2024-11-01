using Microsoft.AspNetCore.Mvc;

namespace SiteRBC.Controllers.Users
{
    public class SignInUsersController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }
    }
}
