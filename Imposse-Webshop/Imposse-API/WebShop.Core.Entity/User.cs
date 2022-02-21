using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Core.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsAdmin { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public List<Story> Stories { get; set; }
        public List<Review> Reviews { get; set; }
        public Basket Basket { get; set; }

    }
}
