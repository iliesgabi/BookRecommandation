using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementAndReporting.Models
{
    public class User
    {
        public User()
        {
            ReadBooks = new List<Book>();
            ActiveBooks = new List<Book>();
            WishList = new List<Book>();
            RecommendedBooks = new List<Book>();
        }

        public int Id { get; set; }

        public int IsAdmin { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public List<Book> ReadBooks { get; set; }

        public List<Book> ActiveBooks { get; set; }

        public List<Book> WishList { get; set; }

        public List<Book> RecommendedBooks { get; set; }
    }
}