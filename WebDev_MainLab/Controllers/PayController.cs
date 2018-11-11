using System;
using System.Collections.Generic;
using WebDev_MainLab.Attributes;
using WebDev_MainLab.Models;
using Microsoft.AspNetCore.Mvc;
using WebDev_MainLab.Data;

namespace WebDev_MainLab.Controllers
{
    public class PayController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PayController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[OrderExist]
        public IActionResult Card()
        {
            return View();
        }

        [HttpPost]
        //[OrderExist]
        public IActionResult Create([Bind("CardNumber, OwnerName, OwnerSurname")]CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                //var order = HttpContext.Session.Get<Order>("Order");
                var item = _context.getByID(6);
                var order = new Order()
                {
                    Adress = "ade",
                    UserID = "idqwr",
                    Name = "anme", 
                    Surname = "sre",
                    TotalPrice = 1.2F,
                    Country = "sdsd",
                    State = "qwe"
                };
                int orderID = _context.AddOrder(order);
                //var lines = TempData["CartLines"] as List<CartLine>;
                List<CartLine> lines = new List<CartLine>() {
                    new CartLine() { MyItem = item, Quantity = 1 },
                     new CartLine() { MyItem = item, Quantity = 2 },
                      new CartLine() { MyItem = item, Quantity = 3 }
                };
                _context.AddOrderItems(orderID, lines);
                TempData["message"] = "Payment successfull. Your order is in process";
                return RedirectToAction(nameof(HomeController.Index), "Home");

            }
            return View(creditCard);
        }
    }
}