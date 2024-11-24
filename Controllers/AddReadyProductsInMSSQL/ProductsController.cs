using Microsoft.AspNetCore.Mvc;
using SiteRBC.Models.Data;

namespace SiteRBC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SiteRBCContext _context;

        public ProductsController(SiteRBCContext context)
        {
            _context = context;
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReadyProduct product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("AdminMenu", "SignInAdmin");
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("AdminMenu", "SignInAdmin"); // Повернення до головної сторінки після видалення
        }
    }
}