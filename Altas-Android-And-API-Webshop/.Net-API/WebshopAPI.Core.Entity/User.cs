using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopAPI.Core.Entity
{
    public class User
    {
        public string IdToken { get; set; }
        public string Email { get; set; }
        public int ExpiresIn { get; set; }
        public string LocalId { get; set; }
    }
}
