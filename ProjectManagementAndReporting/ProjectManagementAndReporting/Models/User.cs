using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementAndReporting.Models
{
    public class User
    {
        public int Id { get; set; }

        public int IsAdmin { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public List<Book> ReadBooks { get; set; }

        public List<Book> ActiveBooks { get; set; }

        public List<Book> WishList { get; set; }
    }
}