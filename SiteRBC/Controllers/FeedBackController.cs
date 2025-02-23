using Microsoft.AspNetCore.Mvc;

namespace SiteRBC.Controllers
{
    public class FeedBackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
