using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;

namespace SiteRBC.Controllers
{
	public class ReadyListController : Controller
    {
        private readonly SiteRBCContext _context;

        public ReadyListController(SiteRBCContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Product()
        {
            //Loading data with database
            List<ReadyProduct> products = await _context.Products.ToListAsync();
            return View(products);
        }
    }
}
