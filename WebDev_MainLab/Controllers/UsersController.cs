using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDev_MainLab.Data;
using WebDev_MainLab.Models;

namespace WebDev_MainLab.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Users
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View(_context.Users.Where(x => x.UserName != "admin").ToList());
        }

        public async Task<IActionResult> LockOut(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.Users
                .SingleOrDefault(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            user.LockoutEnabled = true;

            user.LockoutEnd = await _userManager.IsLockedOutAsync(user) ? DateTime.UtcNow : DateTime.UtcNow.AddMinutes(40);
            await _userManager.UpdateAsync(user);

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }


        // GET: Users/Delete/5
        [Authorize(Roles = "admin")]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.Users
                .SingleOrDefault(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5\
        [Authorize(Roles ="admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
