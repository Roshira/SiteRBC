using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;
using Microsoft.AspNetCore.Authorization;

namespace SiteRBC.Controllers.Admin
{
	public class AdminFunctionalController : Controller
	{
		private readonly SiteRBCContext _context;

		public AdminFunctionalController(SiteRBCContext context)
		{
			_context = context;
		}
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AdminMenu()
		{
			//Loading data with database
			List<ReadyProduct> products = await _context.Products.ToListAsync();
			return View(products);
		}

	}
}