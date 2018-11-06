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
            return Json(new SelectList(list, "ID", "Name"));
        }

        public JsonResult getStates(int id)
        {
            var list = _context.State.Where(x => x.CountryID == id).ToList();
            return Json(new SelectList(list, "ID", "Name"));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            var user = _context.getUser(User.Identity.Name);
            var profile = new ProfileViewModel();
            profile.Name = user.Name;
            profile.Surname = user.Surname;
            profile.orderslist = _context.Order.Where(x => x.UserID == user.Id).ToList();

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
        public IActionResult Create([Bind("ID,CountryID,StateID,Adress")] Order order)
        {
            if (ModelState.IsValid)
            {
                var ovm = new OrderViewModel();
                ovm.Country = _context.Country.FirstOrDefault(x => x.ID == order.CountryID).Name;
                ovm.City = _context.State.FirstOrDefault(x => x.ID == order.StateID).Name;
                ovm.Adress = order.Adress;

                var user = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                order.UserID = user.Id;
                ovm.Name = user.Name;
                ovm.Surname = user.Surname;

                var cart = HttpContext.Session.Get<Cart>("Cart");
                order.Items = cart.Lines;
                ovm.TotalPrice = cart.ComputeTotalValue();
                
                return View("OrderView", ovm);
            }
            return View(order);
        }
                
    }
}
