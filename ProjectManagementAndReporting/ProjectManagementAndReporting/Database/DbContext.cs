using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using Newtonsoft.Json;
using ProjectManagementAndReporting.Models;

namespace ProjectManagementAndReporting.Database
{
    public class DbContext
    {
        private Data _data;

        public Data Data => _data;

        public DbContext()
        {
            GetData();
        }

        public void GetData()
        {
           var allBooks = File.ReadAllText("C:\\allBooks.txt");
           var allUsers = File.ReadAllText("C:\\allUsers.txt");
           var allComments = File.ReadAllText("C:\\allComments.txt");

            _data = new Data
           {
               Books = JsonConvert.DeserializeObject<List<Book>>(allBooks),
               Comments = JsonConvert.DeserializeObject<List<Comment>>(allComments),
               Users = JsonConvert.DeserializeObject<List<User>>(allUsers)
            };

        }

        public void Save()
        {
            File.WriteAllText("C:\\allBooks.txt", JsonConvert.SerializeObject(Data.Books));
            File.WriteAllText("C:\\allComments.txt", JsonConvert.SerializeObject(Data.Comments));
            File.WriteAllText("C:\\allUsers.txt", JsonConvert.SerializeObject(Data.Users));
        }
    }
}