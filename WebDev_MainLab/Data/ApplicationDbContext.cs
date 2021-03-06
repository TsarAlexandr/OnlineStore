﻿using System.Collections.Generic;
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

        #region IRepository

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

        public void AddComments(Commentar comment)
        {
            Commentar.Add(comment);
            this.SaveChanges();
        }

        public ApplicationUser getUser(string userName)
        {
            return Users.FirstOrDefault(x => x.UserName == userName);
        }

        public int DeleteComments(int? id)
        {
            var comment = Commentar.First(x => x.ID == id);
            int itemID = comment.GoodsID;
            Commentar.Remove(comment);
            this.SaveChanges();
            return itemID;
        }
        #endregion

        public int AddOrder(Order order)
        {
            Order.Add(order);
            SaveChanges();
            return Order.Last().ID;
        }

        public void AddOrderItems(int orderID, List<CartLine> lines)
        {
            foreach (var line in lines)
            {
                OrdersItems.Add(new OrdersItems()
                {
                    GoodsId = line.MyItem.ID,
                    OrderId = orderID,
                    Quantity = line.Quantity
                });
            }
            SaveChanges();
        }

        public DbSet<Goods> Goods { get; set; }

        public DbSet<State> State { get; set; }

        public DbSet<Country> Country { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Commentar> Commentar { get; set; }

        public DbSet<OrdersItems> OrdersItems { get; set; }

        List<Goods> IRepository.Goods => Goods.ToList();
        List<Commentar> IRepository.Comments => Commentar.ToList();
       
    }
}
