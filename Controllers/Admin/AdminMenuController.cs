using Microsoft.AspNetCore.Mvc;

namespace SiteRBC.Controllers.Admin
{
	public class AdminMenuController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
