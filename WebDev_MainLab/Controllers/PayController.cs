﻿using System;
using System.Collections.Generic;
using WebDev_MainLab.Attributes;
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

        [OrderExist]
        public IActionResult Card()
        {
            return View();
        }

        [HttpPost]
        [OrderExist]
        public IActionResult Create([Bind("CardNumber, OwnerName, OwnerSurname")]CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                var ovm = HttpContext.Session.Get<Order>("Order");
                
                
                ovm.Name = creditCard.OwnerName;
                ovm.Surname = creditCard.OwnerSurname;
                
                return View("OrderView", ovm);

            }
            return View(creditCard);
        }
    }
}