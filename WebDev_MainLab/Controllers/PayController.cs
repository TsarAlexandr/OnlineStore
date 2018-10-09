﻿using System;
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

        [HttpPost]
        public IActionResult Create([Bind("CardNumber, OwnerNam, OwnerSurname")]CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                var ovm = HttpContext.Session.Get<OrderViewModel>("Order");
                if (ovm == null)
                    return RedirectToAction("Create", "Order");

                ovm.CardNumber = creditCard.CradNumber;
                ovm.Name = creditCard.OwnerName;
                ovm.Surname = creditCard.OwnerSurname;

                HttpContext.Session.Set("Order", null);
                return View("OrderView", ovm);
            }
            return View(creditCard);
        }
    }
}