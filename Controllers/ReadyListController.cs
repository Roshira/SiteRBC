using Microsoft.AspNetCore.Mvc;

namespace SiteRBC.Controllers
{
	public class ReadyListController : Controller
	{
		public IActionResult Product()
		{
			return View();
		}
	}
}
