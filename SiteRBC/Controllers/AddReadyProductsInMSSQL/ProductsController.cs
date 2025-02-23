using Microsoft.AspNetCore.Mvc;
using SiteRBC.Models.Data;

namespace SiteRBC.Controllers
{
    /**
     * @class ProductsController
     * @brief Controller for managing product-related operations.
     */
    public class ProductsController : Controller
    {
        private readonly SiteRBCContext _context;

        /**
         * @brief Constructor for ProductsController.
         * @param context Database context for accessing product data.
         */
        public ProductsController(SiteRBCContext context)
        {
            _context = context;
        }

        /**
         * @brief Displays the product creation form.
         * @return View for creating a new product.
         */
        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        /**
         * @brief Handles the creation of a new product.
         * @param product The product data submitted by the user.
         * @return Redirects to the AdminMenu if successful, otherwise reloads the creation view.
         */
        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReadyProduct product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("AdminMenu", "AdminFunctional");
            }
            return View(product);
        }

        /**
         * @brief Deletes a product by its ID.
         * @param id The ID of the product to be deleted.
         * @return Redirects to the AdminMenu after deletion.
         */
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("AdminMenu", "AdminFunctional"); // Redirecting to the main page after deletion
        }
    }
}
