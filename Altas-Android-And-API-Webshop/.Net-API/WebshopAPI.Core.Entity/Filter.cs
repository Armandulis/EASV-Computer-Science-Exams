using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopAPI.Core.Entity
{
    public class Filter
    {
        public string LastItemId { get; set; }
        public string OrderBy { get; set; }
        public string SearchWord { get; set; }
        public int Amount { get; set; } = 0;
    }
}
