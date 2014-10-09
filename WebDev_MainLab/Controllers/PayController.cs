using System;
using System.Collections.Generic;
using System.Linq;
using WebDev_MainLab.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebDev_MainLab.Controllers
{
    public class PayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Card()
        {
            return View("Credit card");
        }

        //[HttpPost]
        //public IActionResult Create([Bind("CardNumber, OwnerNam, OwnerSurname")]CreditCard creditCard)
        //{
        //    if (ModelState.IsValid)
        //    {

        //    }
        //}
    }
}