using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Core.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
