﻿using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDev_MainLab.Models;

namespace WebDev_MainLab.Controllers
{
    public class GoodsController : Controller
    {
        private readonly IRepository repo;

        public GoodsController(IRepository _repo)
        {
            repo = _repo;
        }

        // GET: Goods
        public IActionResult Index()
        {
            return View(repo.Goods);
        }

        public IActionResult IndexSearch(string SearchString)
        {
            var searchResults = repo.Goods.Any(x => x.Name.Contains(SearchString));
            return View("Index", searchResults);
        }

        public IActionResult IndexCategory(Categories category)
        {
            if (category != 0)
                return View("Index", repo.Goods.Where(x => x.category == category).ToList());
            return View("Index", repo.Goods);
        }


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
        public IActionResult Create([Bind("ID,Rating,Name,Description,Price,category")] Goods goods, IFormFile ImageMimeType)
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
        public IActionResult Edit(int id, [Bind("ID,Rating,Name,Description,Price,category")] Goods goods, IFormFile ImageMimeType)
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