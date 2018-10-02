using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDev_MainLab.Data;
using WebDev_MainLab.Models;

namespace WebDev_MainLab.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public JsonResult getCity(int id)
        {
            var list = new List<State>();
            list = _context.State.Where(city => city.CountryID == id).ToList();
            return Json(new SelectList(list, "ID", "City"));
        }

        // GET: Orders
        public IActionResult Index()
        {
            return View(_context.Order.ToList());
        }

        

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,Adress")] Order order, int countryId, int stateId)
        {
            if (ModelState.IsValid)
            {

                order.Country = _context.Country.FirstOrDefault(x => x.ID == countryId);
                order.State = _context.State.FirstOrDefault(x => x.ID == stateId);

                var cart = HttpContext.Session.Get<Cart>("Cart");
                if (cart != null)
                {
                    cart.Clear();
                    HttpContext.Session.Set("Cart", cart);
                }

                TempData["message"] = "Order Passed! Address:" + order.Country.Name + " "
                    + order.State.Name + " " +
                    order.Adress;
                return RedirectToAction("Index", "Home");
            }
            return View(order);
        }

       


        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.ID == id);
        }
    }
}
