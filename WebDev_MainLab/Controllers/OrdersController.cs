using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public JsonResult getCountries()
        {
            var list = _context.Country.ToList();
            return Json(new SelectList(list, "ID", "Name"));
        }

        public JsonResult getStates(int id)
        {
            var list = _context.State.Where(x => x.CountryID == id).ToList();
            return Json(new SelectList(list, "ID", "Name"));
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
        public IActionResult Create([Bind("ID,CountryID,StateID,Adress")] Order order)
        {
            if (ModelState.IsValid)
            {
                var ovm = GetOrder();
                ovm.Country = _context.Country.FirstOrDefault(x => x.ID == order.CountryID).Name;
                ovm.City = _context.State.FirstOrDefault(x => x.ID == order.StateID).Name;
                ovm.Adress = order.Adress;

                var cart = HttpContext.Session.Get<Cart>("Cart");
                if (cart != null)
                {
                    cart.Clear();
                    HttpContext.Session.Set("Cart", cart);
                }

                HttpContext.Session.Set("Order", ovm);
                
                return RedirectToAction("Index", "Pay");
            }
            return View(order);
        }

        private OrderViewModel GetOrder()
        {
            var order = HttpContext.Session.Get<OrderViewModel>("Order");
            if (order == null)
            {
                order = new OrderViewModel();
                HttpContext.Session.Set("Order", order);
            }

            return order;
        }

    }
}
