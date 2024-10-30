using Microsoft.AspNetCore.Mvc;

namespace SiteRBC.Controllers
{
	public class SignInUsers : Controller
	{
		public IActionResult SignIn()
		{
			return View();
		}
	}
}
