using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebDev_MainLab.Models;

namespace WebDev_MainLab.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>,IRepository
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public void RemoveItem(int? id)
        {
            if (id != null)
            {
                var item = getByID(id);
                Goods.Remove(item);
                this.SaveChanges();
            }
        }

        public void AddItem(Goods item)
        {
            if (item != null)
            {
                Goods.Add(item);
                this.SaveChanges();
            }
        }

        public Goods getByID(int? id)
        {
            if (id != null)
            {
                var res = Goods.FirstOrDefault(x => x.ID == id);
                return res;
            }
            return null;
        }

        public void UpdateItem(Goods item)
        {
            Goods.Update(item);
            this.SaveChanges();
        }

        public DbSet<Goods> Goods { get; set; }

        public DbSet<State> State { get; set; }

        public DbSet<Country> Country { get; set; }

        public DbSet<OrderViewModel> Order { get; set; }

        List<Goods> IRepository.Goods => Goods.ToList();
    }
}
