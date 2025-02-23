using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
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
            // Завантажуємо продукти з БД
            List<ReadyProduct> products = await _context.Products.ToListAsync();
            return View(products);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> FeedBack()
        {
            // Завантажуємо список користувачів з БД
            List<UsersInfo> usersInfo = await _context.UsersInfo.ToListAsync();
            return View(usersInfo);
        }
    }
}
