using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Core.Entity
{
    public class Review
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }
    }
}
