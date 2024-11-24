using Microsoft.AspNetCore.Mvc;

namespace SiteRBC.Controllers.Features
{
    public class FeaturesController : Controller
    {
        public IActionResult GenerelPageFeatures()
        {
            return View();
        }
    }
}
