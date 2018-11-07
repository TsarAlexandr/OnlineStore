using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebDev_MainLab.Data;
using WebDev_MainLab.Models;
using WebDev_MainLab.Attributes;
using Microsoft.AspNetCore.Authorization;

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
            return Json(new SelectList(list, "Name", "Name"));
        }

        public JsonResult getStates(int id)
        {
            var list = _context.State.Where(x => x.CountryID == id).ToList();
            return Json(new SelectList(list, "Name", "Name"));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            var name = User.Identity.Name;
            var userID = _context.getUser(name).Id;
            var profile = new ProfileViewModel()
            {
                Name = name,
                OrdersList = _context.Order.Where(x => x.UserID == userID).ToList()
            };
            return View(profile);

        }
       // GET: Orders/Create
        [CartNotEmpty]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [CartNotEmpty]
        public IActionResult Create([Bind("ID,Country,State,Adress,Name,Surname")] Order order)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                order.UserID = user.Id;
                
                var cart = HttpContext.Session.Get<Cart>("Cart");
                order.Items = cart.Lines;
                order.TotalPrice = cart.ComputeTotalValue();

                HttpContext.Session.Set("Order", order);
                
                return View("OrderView", order);
            }
            return View(order);
        }
                
    }
}
