using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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
				HttpContext.Session.SetString("IsAuthenticated", "true");
				return RedirectToAction("AdminMenu", "SignInAdmin");
			}
			ViewBag.ErrorMessage = "Error with password";
			return View();
		}
		public IActionResult AdminMenu()
		{
			if (HttpContext.Session.GetString("IsAuthenticated") != "true")
			{
				return RedirectToAction("SignInAdmin", "SignInAdmin");
			}
			return View();
		}

	}
}