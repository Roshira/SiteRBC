using Microsoft.AspNetCore.Mvc;
using SiteRBC.Models;
using System.Diagnostics;

namespace SiteRBC.Controllers
{
    /**
     * @class HomeController
     * @brief Controller for handling the main page and error handling.
     */
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /**
         * @brief Constructor for HomeController.
         * @param logger Logger for tracking application events.
         */
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /**
         * @brief Displays the tour page.
         * @return View of the tour page.
         */
        public IActionResult Tour()
        {
            return View();
        }

        /**
         * @brief Handles errors and returns the error page with error details.
         * @return View of the error page.
         */
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
