using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShop.Core.DomainService;
using WebShop.Core.Entity;

namespace WebShop.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly WebShopContext _ctx;

        public ProductRepository(WebShopContext ctx)
        {
            _ctx = ctx;
        }

        public Product CreateProduct(Product product)
        {
            _ctx.Products.Attach(product).State = EntityState.Added;
            _ctx.SaveChanges();

            return product;
        }

        public Product DeleteProduct(int id)
        {
            var productToDelete = _ctx.Products.FirstOrDefault(p => p.Id == id);
            if (productToDelete != null)
            {
                _ctx.Products.Attach(productToDelete).State = EntityState.Deleted;
                _ctx.SaveChanges();
                return productToDelete;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Product> GetAllProducts(Filter filter)
        {
            if (filter != null)
            {
                return _ctx.Products.Skip(filter.ItemsPerPage * (filter.CurrentPage - 1)).Take(filter.ItemsPerPage);
            }
            else
            {
                return _ctx.Products;
            }
        }

        public Product GetProductById(int id)
        {
            return _ctx.Products.Include(p => p.Reviews).ThenInclude(r => r.User).FirstOrDefault(p => p.Id == id);
        }

        public Product UpdateProduct(Product product)
        {
            _ctx.Products.Attach(product).State = EntityState.Modified;
            _ctx.SaveChanges();
            return product;
        }
    }
}
