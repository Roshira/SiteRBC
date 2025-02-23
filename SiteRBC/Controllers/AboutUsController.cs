using Microsoft.AspNetCore.Mvc;

namespace SiteRBC.Controllers.AboutUs
{
    /**
    * @class AccountsController
    * @brief Controller with inf about company
    */
    public class AboutUsController : Controller
    {
        public IActionResult MainInformation()
        {
            return View();
        }
    }
}
