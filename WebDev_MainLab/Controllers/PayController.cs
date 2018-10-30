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
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("CardNumber, OwnerName, OwnerSurname")]CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                //var ovm = HttpContext.Session.Get<OrderViewModel>("Order");
                //if (ovm == null)
                //    return RedirectToAction("Create", "Orders");
                var ovm = new OrderViewModel() { Country = "Belarus", City = "Minsk", Adress = "Adress"};
                ovm.CardNumberFirst = creditCard.CardNumber.Substring(0, 4);
                ovm.CardNumberLast = creditCard.CardNumber.Substring(creditCard.CardNumber.Length - 4);
                ovm.Name = creditCard.OwnerName;
                ovm.Surname = creditCard.OwnerSurname;
                //----------------------------------------
                HttpContext.Session.Set("Order", ovm);
                HttpContext.Session.SetNull("Order");
                var newOvm = HttpContext.Session.Get<OrderViewModel>("Order");
                //---------------------------------------------------------------
                return View("OrderView", ovm);

            }
            return View(creditCard);
        }
    }
}