using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Core.Entity;

namespace WebShop.Infrastructure.Data
{
    public class WebShopContext : DbContext
    {
        

        public WebShopContext(DbContextOptions<WebShopContext> option) : base(option)
        {

        }

        public WebShopContext(): base()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>().HasOne(r => r.User).WithMany(u => u.Reviews).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Review>().HasOne(r => r.Product).WithMany(p => p.Reviews).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Story>().HasOne(story => story.User).WithMany(user => user.Stories).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Basket>().HasOne(b => b.User).WithOne(u => u.Basket).HasForeignKey<Basket>("User.Id").OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Basket>().HasMany(b => b.Products);
        }
        
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }

    }
}