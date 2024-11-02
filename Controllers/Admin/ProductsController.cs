using Microsoft.AspNetCore.Mvc;
using SiteRBC.Models.Data;
using System.Threading.Tasks;

namespace SiteRBC.Controllers
{
	public class ProductsController : Controller
	{
		private readonly ReadyProductContext _context;

		public ProductsController(ReadyProductContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> Create(ReadyProductcs product)
		{
			if (ModelState.IsValid)
			{
				_context.Products.Add(product);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index"); // Перенаправлення на сторінку списку продуктів
			}
			return View(product); // Повернення на сторінку, якщо модель некоректна
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var product = await _context.Products.FindAsync(id);
			if (product == null)
			{
				return NotFound();
			}

			_context.Products.Remove(product);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
	}
}