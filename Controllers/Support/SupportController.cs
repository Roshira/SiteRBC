﻿using Microsoft.AspNetCore.Mvc;

namespace SiteRBC.Controllers.Support
{
    public class SupportController : Controller
    {
        public IActionResult GeneralSupportPage()
        {
            return View();
        }
    }
}