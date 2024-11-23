using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;

namespace SiteRBC.Controllers.Admin
{
	public class AdminFunctionalController : Controller
	{
		private readonly ReadyProductContext _context;

		public AdminFunctionalController(ReadyProductContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> AdminMenu()
		{
			if (HttpContext.Session.GetString("IsAuthenticated") != "true")
			{
				return RedirectToAction("SignInAdmin");
			}

			//Loading data with database
			List<ReadyProductcs> products = await _context.Products.ToListAsync();
			return View(products);
		}

	}
}