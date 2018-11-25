using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebDev_MainLab.Models;

namespace WebDev_MainLab.Controllers
{
    public class CartsController : Controller
    {
        private IRepository repo;
        private readonly IStringLocalizer<CartsController> _localizer;
        public CartsController(IRepository repos, IStringLocalizer<CartsController> localizer)
        {
            repo = repos;
            _localizer = localizer;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View(GetCart());
        }

        public IActionResult getOrder()
        {
            var cart = GetCart();
            if (cart.Lines.Count == 0)
            {
                TempData["error"] = _localizer["NoItems"]; 
                return View("Index", cart);
            }
            return RedirectToAction("Create", "Orders");
        }

        [HttpPost]
        public IActionResult AddItem(int itemID)
        {
            var item = repo.getByID(itemID);
            if (item != null)
            {
                var cart = GetCart();
                cart.AddItem(item, 1);
                HttpContext.Session.Set("Cart", cart);
            }

            return RedirectToAction("Index", "Carts");

        }
        [Authorize]
        [HttpPost]
        public IActionResult RemoveItem(int itemID)
        {
            var film = repo.getByID(itemID);
            if (film != null)
            {
                var cart = GetCart();
                cart.RemoveLine(film);
                HttpContext.Session.Set("Cart", cart);
            }
            return RedirectToAction("Index");

        }

        public Cart GetCart()
        {
            Cart cart = HttpContext.Session.Get<Cart>("Cart");
            if (cart == null)
            {
                cart = new Cart();
                cart.Lines = new List<CartLine>();
            }
            return cart;
        }
    }
}