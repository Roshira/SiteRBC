using Microsoft.AspNetCore.Mvc;

namespace SiteRBC.Controllers.Features
{
    /**
    * @class AccountsController
    * @brief Controller with Features of Our Bomb Shelters
    */
    public class FeaturesController : Controller
    {
        public IActionResult GenerelPageFeatures()
        {
            return View();
        }
    }
}
