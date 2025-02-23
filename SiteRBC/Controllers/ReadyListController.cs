using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteRBC.Controllers
{
    /**
     * @class ReadyListController
     * @brief Controller for managing the list of ready products and saving user information.
     */
    public class ReadyListController : Controller
    {
        private readonly SiteRBCContext _context;

        /**
         * @brief Constructor for ReadyListController.
         * @param context Database context of type SiteRBCContext.
         */
        public ReadyListController(SiteRBCContext context)
        {
            _context = context;
        }

        /**
         * @brief Retrieves the list of ready products from the database.
         * @return View containing the list of products.
         */
        public async Task<IActionResult> Product()
        {
            // Loading data from the database
            List<ReadyProduct> products = await _context.Products.ToListAsync();
            return View(products);
        }

        /**
         * @brief Saves user information into the database.
         * @param userInfo Object of type UsersInfo containing user details.
         * @return JSON response with a message indicating the operation result.
         */
        [HttpPost]
        public async Task<IActionResult> SaveUserInfo([FromBody] UsersInfo userInfo)
        {
            if (userInfo == null || string.IsNullOrEmpty(userInfo.Number))
            {
                return Json(new { message = "Invalid data" });
            }

            // Saving to the database
            _context.UsersInfo.Add(userInfo);
            await _context.SaveChangesAsync();

            return Json(new { message = "Your request has been saved!" });
        }
    }
}
