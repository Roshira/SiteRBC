using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;

namespace SiteRBC.Controllers.Admin
{
	public class SignInAdminController : Controller
	{
		private readonly ReadyProductContext _context;

		public SignInAdminController(ReadyProductContext context)
		{
			_context = context;
		}
		[HttpGet]
		public IActionResult SignInAdmin()
		{
			return View();
		}

		[HttpPost]
		public IActionResult SignInAdmin(string email, string password)
		{
			if (email == "roshira@ukr.net" && password == "123qwe123qwe")
			{
				HttpContext.Session.SetString("IsAuthenticated", "true");
				return RedirectToAction("AdminMenu", "SignInAdmin");
			}
			ViewBag.ErrorMessage = "Error with password";
			return View();
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