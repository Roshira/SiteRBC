using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;

namespace SiteRBC.Controllers
{
	public class ReadyListController : Controller
    {
        private readonly ReadyProductContext _context;

        public ReadyListController(ReadyProductContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Product()
        {
            //Loading data with database
            List<ReadyProductcs> products = await _context.Products.ToListAsync();
            return View(products);
        }
        public IActionResult CallBack()
        {

            return View();
        }
    }
}
