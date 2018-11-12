using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDev_MainLab.Models;
using WebDev_MainLab.Models.GoodsEntities;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace WebDev_MainLab.Controllers
{
    public class GoodsController : Controller
    {
        private readonly IRepository repo;

        public GoodsController(IRepository _repo)
        {
            repo = _repo;
        }
        #region Indexes
        // GET: Goods
        public IActionResult Index()
        {
            return View(repo.Goods);
        }

        public IActionResult IndexSearch(string SearchString)
        {
            var searchResults = repo.Goods.Where(x => x.Name.Contains(SearchString));
            return View("Index", searchResults);
        }

        public IActionResult IndexCategory(Categories category)
        {
            if (category != 0)
                return View("Index", repo.Goods.Where(x => x.Category == category).ToList());
            return View("Index", repo.Goods);
        }
        public IActionResult AdminIndex()
        {
            return View("AdminIndex", repo.Goods.ToList());
        }
        #endregion

        #region JsonSerialize
        public void SerializeElectronics([Bind("Power,CPU,Memory,OS")]Electronics electronic)
        {
            TempData["params"] = JsonConvert.SerializeObject(electronic);
        }
        public void SerializeBooks([Bind("Author,ISBN,PagesCount")]Books book)
        {
            TempData["params"] = JsonConvert.SerializeObject(book);
        }
        public void SerializeClothes([Bind("Size,Material,Color,Sex")]Clothes clothe)
        {
            TempData["params"] = JsonConvert.SerializeObject(clothe);
        }
        public void SerializeToys([Bind("Age,Color")]Toys toy)
        {
            TempData["params"] = JsonConvert.SerializeObject(toy);
        }
        #endregion

        #region GoodsPartialViews
        public IActionResult GetInPartialView(int id)
        {
            var name = Enum.GetNames(typeof(Categories))[id];
            return PartialView($"InputPartialViews/_{name}InPartial");
        }
        public IActionResult GetOutPartialView(string cat)
        {
            var par = TempData["param"] as string;
            Type type = Type.GetType($"WebDev_MainLab.Models.GoodsEntities.{cat}");
            var paramObj = JsonConvert.DeserializeObject(par, type);
            return PartialView($"OutputPartialViews/_{cat}OutPartial",paramObj);
        }

        public IActionResult GetCommentsPartial(int itemID)
        {
            var comments = new List<Commentar>();
            comments = repo.Comments.Where(x => x.GoodsID == itemID).ToList();
            return PartialView("_CommentsPartial", comments);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddComment(string text, int itemID)
        {
            var comment = new Commentar()
            {
                Text = text,
                GoodsID = itemID,
                UserName = User.Identity.Name
            };

            repo.AddComments(comment);
            return GetCommentsPartial(itemID);
        }
        #endregion

        // GET: Goods/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goods = repo.getByID(id);
            
            if (goods == null)
            {
                return NotFound();
            }

            TempData["param"] = goods.AdditionalParameters;

            return View(goods);
        }

        // GET: Goods/Create
        
        public IActionResult Create()
        {
            return View();
        }

        // POST: Goods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Rating,Name,Description,Price,Category")] Goods goods, IFormFile ImageMimeType)
        {
            if (ModelState.IsValid)
            {
                if (ImageMimeType != null)
                {
                    byte[] imageData = null;

                    using (var binaryReader = new BinaryReader(ImageMimeType.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)ImageMimeType.Length);
                    }

                    goods.ImageData = imageData;
                }
                goods.AdditionalParameters = TempData["params"] as string;
               
                repo.AddItem(goods);
                return RedirectToAction(nameof(Index));
            }
            return View(goods);
        }

        // GET: Goods/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goods = repo.getByID(id);
            if (goods == null)
            {
                return NotFound();
            }
            return View(goods);
        }

        // POST: Goods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Rating,Name,Description,Price,category")] Goods goods, IFormFile ImageMimeType)
        {
            if (id != goods.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageMimeType != null)
                    {
                        byte[] imageData = null;

                        using (var binaryReader = new BinaryReader(ImageMimeType.OpenReadStream()))
                        {
                            imageData = binaryReader.ReadBytes((int)ImageMimeType.Length);
                        }

                        goods.ImageData = imageData;
                    }
                    repo.UpdateItem(goods);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoodsExists(goods.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(goods);
        }

        // GET: Goods/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goods = repo.getByID(id);
            if (goods == null)
            {
                return NotFound();
            }

            return View(goods);
        }

        // POST: Goods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            repo.RemoveItem(id);
            return RedirectToAction(nameof(Index));
        }

       
        private bool GoodsExists(int id)
        {
            return repo.Goods.Any(e => e.ID == id);
        }
    }
}
