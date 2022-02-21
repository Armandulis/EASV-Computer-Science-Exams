using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShop.Core.DomainService;
using WebShop.Core.Entity;

namespace WebShop.Infrastructure.Data.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly WebShopContext _ctx;

        public BasketRepository(WebShopContext context)
        {
            _ctx = context;
        }

        public Basket DeleteBasket(int id)
        {
            var basket = GetBasket(id);
            _ctx.Baskets.Attach(basket).State = EntityState.Deleted;
            _ctx.SaveChanges();
            return basket;
        }

        public void DeleteFromBasket(int id, Product product)
        {
            var basket = GetBasketIncludeUserAndProducts(id);
            product = basket.Products.FirstOrDefault(p => p.Id == product.Id);
            basket.Products.Remove(product);
            _ctx.Entry(basket).Collection(b => b.Products).IsModified = true;
            _ctx.Attach(basket).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public Basket GetBasket(int id)
        {
            return _ctx.Baskets.FirstOrDefault(b => b.User.Id == id);
        }

        public Basket GetBasketIncludeUserAndProducts(int id)
        {
            return _ctx.Baskets.Include(b => b.User).Include(b => b.Products).FirstOrDefault(b => b.User.Id == id);
        }

        public Basket NewBasket(Basket basket)
        {
            if (basket.User != null && (_ctx.ChangeTracker.Entries<Basket>().FirstOrDefault(pe => pe.Entity.Id == basket.User.Id)) == null)
            {
                _ctx.Attach(basket.User);
            }

            _ctx.Entry(basket).Reference(b => b.User).IsModified = true;
            _ctx.Baskets.Attach(basket).State = EntityState.Added;
            _ctx.SaveChanges();

            return basket;
        }

        public void UpdateBasket(Basket basket, Product product)
        {
            if (product != null)
            {
                _ctx.Entry(product).State = EntityState.Unchanged;
                basket.Products.Add(product);
            }
            _ctx.Entry(basket).Collection(b => b.Products).IsModified = true;
            _ctx.Baskets.Attach(basket).State = EntityState.Modified;
            _ctx.SaveChanges();
        }
    }
}
