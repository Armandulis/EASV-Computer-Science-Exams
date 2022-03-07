using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopAPI.Core.Entity
{
    public class Product
    {
        public string Id { get; set; }
        public string Description { get; set; }

        public string Title { get; set; }
        public string Brand { get; set; }

        public string Type { get; set; }
        public string Price { get; set; }

        public string PictureUrl { get; set; }

        public string Picture { get; set; }
        public string Amount { get; set; }


    }
}
