using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace SiteRBC.Controllers.Admin
{
    /**
     * @class AdminFunctionalController
     * @brief Controller for handling administrative functionalities.
     */
    public class AdminFunctionalController : Controller
    {
        private readonly SiteRBCContext _context;

        /**
         * @brief Constructor for AdminFunctionalController.
         * @param context Database context for accessing stored data.
         */
        public AdminFunctionalController(SiteRBCContext context)
        {
            _context = context;
        }

        /**
         * @brief Displays the admin menu with a list of available products.
         * @return View containing the list of products.
         * @note Only accessible by users with the "Admin" role.
         */
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminMenu()
        {
            // Load products from the database
            List<ReadyProduct> products = await _context.Products.ToListAsync();
            return View(products);
        }

        /**
         * @brief Displays the feedback section with user information.
         * @return View containing the list of users who provided feedback.
         * @note Only accessible by users with the "Admin" role.
         */
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> FeedBack()
        {
            // Load user information from the database
            List<UsersInfo> usersInfo = await _context.UsersInfo.ToListAsync();
            return View(usersInfo);
        }
    }
}
