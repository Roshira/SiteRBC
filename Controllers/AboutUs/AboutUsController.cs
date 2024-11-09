using Microsoft.AspNetCore.Mvc;

namespace SiteRBC.Controllers.AboutUs
{
    public class AboutUsController : Controller
    {
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
