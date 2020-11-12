using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementAndReporting.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string PublishingHouse { get; set; }
        public string ShortDescription { get; set; }
        public float Rating { get; set; }
        public List<Comment> Comments { get; set; }
    }
}