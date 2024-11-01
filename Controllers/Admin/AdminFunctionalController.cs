using Microsoft.AspNetCore.Mvc;

namespace SiteRBC.Controllers.Admin
{
	public class SignInAdminController : Controller
	{
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
				return RedirectToAction("AdminMenu", "SignInAdmin");
			}
			ViewBag.ErrorMessage = "Error with password";
			return View();
		}
		public IActionResult AdminMenu()
		{
			return View();
		}

	}
}