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

        [OrderExist]
        public IActionResult Card()
        {
            return View();
        }

        [HttpPost]
        //[OrderExist]
        public IActionResult Create([Bind("CardNumber, OwnerName, OwnerSurname,ValidDateMonth, ValidDateYear, Code")]CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                var order = HttpContext.Session.Get<Order>("Order");
                var cart = HttpContext.Session.Get<Cart>("Cart");
                order.TotalPrice = cart.ComputeTotalValue();

                int orderID = _context.AddOrder(order);
                
                _context.AddOrderItems(orderID, cart.Lines);

                TempData["message"] = "Payment successfull. Your order is in process";
                return RedirectToAction("Index", "Home");

            }
            return View("Card", creditCard);
        }
    }
}