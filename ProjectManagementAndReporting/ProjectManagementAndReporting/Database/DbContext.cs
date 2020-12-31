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
           var idComment = File.ReadAllText("C:\\idComment.txt");
           var idUser = File.ReadAllText("C:\\idUser.txt");
           var idBook = File.ReadAllText("C:\\idBook.txt");

            _data = new Data
           {
               Books = JsonConvert.DeserializeObject<List<Book>>(allBooks),
               Comments = JsonConvert.DeserializeObject<List<Comment>>(allComments),
               Users = JsonConvert.DeserializeObject<List<User>>(allUsers),
               IdComment = JsonConvert.DeserializeObject<int>(idComment),
               IdBook = JsonConvert.DeserializeObject<int>(idBook),
               IdUser = JsonConvert.DeserializeObject<int>(idUser)
            };

        }

        public void Save()
        {
            File.WriteAllText("C:\\allBooks.txt", JsonConvert.SerializeObject(Data.Books));
            File.WriteAllText("C:\\allComments.txt", JsonConvert.SerializeObject(Data.Comments));
            File.WriteAllText("C:\\allUsers.txt", JsonConvert.SerializeObject(Data.Users));
            File.WriteAllText("C:\\idComment.txt", JsonConvert.SerializeObject(Data.IdComment));
            File.WriteAllText("C:\\idBook.txt", JsonConvert.SerializeObject(Data.IdBook));
            File.WriteAllText("C:\\idUser.txt", JsonConvert.SerializeObject(Data.IdUser));
        }
    }
}