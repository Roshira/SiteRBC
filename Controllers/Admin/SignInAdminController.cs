using Microsoft.AspNetCore.Mvc;

namespace SiteRBC.Controllers.Admin
{
    public class SignInAdminController : Controller
    {
        public IActionResult SignInAdmin()
        {
            return View();
        }
    }
}
