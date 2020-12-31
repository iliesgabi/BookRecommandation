using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectManagementAndReporting.Models;

namespace ProjectManagementAndReporting.Database
{
    public class Data
    {
        public List<User> Users { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Book> Books { get; set; }
        public int IdComment { get; set; }
        public int IdBook { get; set; }
        public int IdUser { get; set; }

    }
}