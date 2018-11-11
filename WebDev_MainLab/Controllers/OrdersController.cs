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

        public JsonResult getStates(string countryName)
        {
            var country = _context.Country.FirstOrDefault(x => x.Name == countryName);
            var list = _context.State.Where(x => x.CountryID == country.ID).ToList();
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
                TotalSpend = 0,
                Items = new List<CartLine>(),
                OrdersList = _context.Order.Where(x => x.UserID == userID).ToList()
            };
            foreach (var order in profile.OrdersList)
            {
                var items = _context.OrdersItems.Where(x => x.OrderId == order.ID).ToList();
                foreach (var item in items)
                {
                    profile.Items.Add(new CartLine()
                    { MyItem = _context.getByID(item.GoodsId),
                        Quantity = item.Quantity
                    });
                }
                profile.TotalSpend += order.TotalPrice;
            }
            
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
        public IActionResult Create([Bind("Country,State,Adress,Name,Surname")] Order order)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                order.UserID = user.Id;

                
                HttpContext.Session.Set("Order", order);

                return RedirectToAction("Index", "Pay");//return View("OrderView", order);
            }
            return View(order);
        }
                
    }
}
