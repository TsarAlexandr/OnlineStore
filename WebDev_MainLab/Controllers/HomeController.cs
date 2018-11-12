using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using WebDev_MainLab.Models;

namespace WebDev_MainLab.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            if (User != null && User.IsInRole("admin"))
                return View("AdminIndex");
            return View();
        }

        public IActionResult About()
        {
           return View();
        }

        public IActionResult Contact()
        {
           return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }
    }
}
