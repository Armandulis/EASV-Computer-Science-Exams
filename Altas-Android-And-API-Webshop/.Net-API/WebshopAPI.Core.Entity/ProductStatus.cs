using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopAPI.Core.Entity
{
    public class ProductStatus
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public Product Product { get; set; }

        public string Status { get; set; }

        public string PurchaseDate { get; set; }
    }
}
