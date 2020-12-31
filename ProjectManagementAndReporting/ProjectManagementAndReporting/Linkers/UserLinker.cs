using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectManagementAndReporting.Database;
using ProjectManagementAndReporting.Models;

namespace ProjectManagementAndReporting.Linkers
{
    public sealed class UserLinker
    {
        private static readonly UserLinker userLinker = new UserLinker();
        private readonly DbContext dataBase;

        private UserLinker()
        {
            this.dataBase = new Database.DbContext();
        }

        public static UserLinker Instance()
        {
            return userLinker;
        }

        public List<User> GetUsers()
        {
            return dataBase.Data.Users;
        }

        public User GetUser(int idUser)
        {
            return dataBase.Data.Users.Find(user => user.Id == idUser);
        }

        public List<Book> GetReadBooks(User user)
        {
            return user.ReadBooks;
        }

        public List<Book> GetActiveBooks(User user)
        {
            return user.ActiveBooks;
        }

        public List<Book> GetWishList(User user)
        {
            return user.WishList;
        }

        public List<User> GetAdmins()
        {
            return dataBase.Data.Users.FindAll(user => user.IsAdmin == 1);
        }

        public int VerifyUser(string username, string password)
        {
            List<User> usernames = dataBase.Data.Users.FindAll(user => user.Username.Equals(username));
            foreach (User user in usernames)
            {
                if (user.Password.Equals(password))
                    return user.IsAdmin;
            }
            return -1;
        }

        public int GetId(string usermane)
        {
            return dataBase.Data.Users.Find(user => user.Username.Equals(usermane)).Id;
        }

        public void AddUser(int isAdmin, string username, string password)
        {
            User user = new User();
            user.Id = dataBase.Data.IdUser;
            dataBase.Data.IdUser = dataBase.Data.Users.Count;
            user.IsAdmin = isAdmin;
            user.Username = username;
            user.Password = password;

            dataBase.Data.Users.Add(user);
            dataBase.Save();
        }

        public void AddReadBook(User user, Book book)
        {
            user.ReadBooks.Add(book);
            dataBase.Save();
        }

        public void AddActiveBook(User user, Book book)
        {
            user.ActiveBooks.Add(book);
            dataBase.Save();
        }

        public void AddBookToWishList(User user, Book book)
        {
            user.WishList.Add(book);
            dataBase.Save();
        }

        public void DeleteUser(User user)
        {
            dataBase.Data.Users.Remove(user);
            dataBase.Save();
        }

        public void DeleteBookFromActiveBooks(User user, int idBook)
        {
            user.ActiveBooks.Remove(user.ActiveBooks.Find(book => book.Id == idBook));
            dataBase.Save();
        }

        public void DeleteBookFromWishList(User user, int idBook)
        {
            user.WishList.Remove(user.WishList.Find(book => book.Id == idBook));
            dataBase.Save();
        }
    }
}