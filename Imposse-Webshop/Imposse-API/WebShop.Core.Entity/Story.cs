using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Core.Entity
{
    public class Story
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public User User { get; set; }


    }
}
