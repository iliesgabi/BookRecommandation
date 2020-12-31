using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectManagementAndReporting.Database;
using ProjectManagementAndReporting.Linkers;
using ProjectManagementAndReporting.Models;

namespace ProjectManagementAndReporting.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserLinker userLinker = UserLinker.Instance();
        private readonly BookLinker bookLinker = BookLinker.Instance();

        [HttpGet]
        [Route("api/user")]
        // GET api/user
        public List<User> GetUsers()
        {
            return userLinker.GetUsers();
        }

        [HttpGet]
        [Route("api/user/id/{idUser}")]
        // GET api/user/id/5
        public User GetUser(int idUser)
        {
            return userLinker.GetUser(idUser);
        }

        [HttpGet]
        [Route("api/user/readBooks/{idUser}")]
        // GET api/user/readBooks/5
        public List<Book> GetReadBooks(int idUser)
        {
            User user = userLinker.GetUser(idUser);
            return userLinker.GetReadBooks(user);
        }

        [HttpGet]
        [Route("api/user/activeBooks/{idUser}")]
        // GET api/user/activeBooks/5
        public List<Book> GetActiveBooks(int idUser)
        {
            User user = userLinker.GetUser(idUser);
            return userLinker.GetActiveBooks(user);
        }

        [HttpGet]
        [Route("api/user/wishList/{idUser}")]
        // GET api/user/wishList/5
        public List<Book> GetWishList(int idUser)
        {
            User user = userLinker.GetUser(idUser);
            return userLinker.GetWishList(user);
        }

        [HttpGet]
        [Route("api/user/admin")]
        // GET api/user/admin
        public List<User> GetAdmins()
        {
            return userLinker.GetAdmins();
        }

        [HttpGet]
        [Route("api/user/{username}")]
        public int GetId(string username)
        {
            return userLinker.GetId(username);
        }

        [HttpGet]
        [Route("api/user/verify/{username}/{password}")]
        public int VerifyUser(string username, string password)
        {
           return userLinker.VerifyUser(username, password);
        }

        [HttpPost]
        [Route("api/user/add/{isAdmin}/{username}/{password}")]
        public HttpResponseMessage AddUser(int isAdmin, string username, string password)
        {
            try
            {
                userLinker.AddUser(isAdmin, username, password);
                return Request.CreateResponse(HttpStatusCode.OK, "User created");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }

        [HttpPut]
        [Route("api/user/readBook/{idUser}/{idBook}")]
        public HttpResponseMessage AddReadBook(int idUser, int idBook)
        {
            try
            {
                User user = userLinker.GetUser(idUser);
                Book book = bookLinker.GetBook(idBook);
                userLinker.AddReadBook(user, book);
                return Request.CreateResponse(HttpStatusCode.OK, "Read Book added");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }

        [HttpPut]
        [Route("api/user/activeBook/{idUser}/{idBook}")]
        public HttpResponseMessage AddActiveBook(int idUser, int idBook)
        {
            try
            {
                User user = userLinker.GetUser(idUser);
                Book book = bookLinker.GetBook(idBook);
                userLinker.AddActiveBook(user, book);
                return Request.CreateResponse(HttpStatusCode.OK, "Active Book added");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }

        [HttpPut]
        [Route("api/user/wishList/{idUser}/{idBook}")]
        public HttpResponseMessage AddBookToWishList(int idUser, int idBook)
        {
            try
            {
                User user = userLinker.GetUser(idUser);
                Book book = bookLinker.GetBook(idBook);
                userLinker.AddBookToWishList(user, book);
                return Request.CreateResponse(HttpStatusCode.OK, "Book to wish list added");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }

        [HttpPut]
        [Route("api/user/rating/{idUser}/{idBook}/{rating}")]
        public HttpResponseMessage AddRatingToBook(int idUser, int idBook, int rating)
        {
            try
            {
                Book book = bookLinker.GetBook(idBook);
                bookLinker.AddRatingToBook(idUser, book, rating);
                return Request.CreateResponse(HttpStatusCode.OK, "Rating added to book");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }

        [HttpDelete]
        [Route("api/user/delete/{idUser}")]
        public HttpResponseMessage DeleteUser(int idUser)
        {
            try
            {
                User user = userLinker.GetUser(idUser);
                userLinker.DeleteUser(user);
                return Request.CreateResponse(HttpStatusCode.OK, "User deleted");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }

        [HttpDelete]
        [Route("api/user/deleteActiveBook/{idUser}/{idBook}")]
        public HttpResponseMessage DeleteBookFromActiveBooks(int idUser, int idBook)
        {
            try
            {
                User user = userLinker.GetUser(idUser);
                userLinker.DeleteBookFromActiveBooks(user, idBook);
                return Request.CreateResponse(HttpStatusCode.OK, "Active book deleted");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }

        [HttpDelete]
        [Route("api/user/deleteWishListBook/{idUser}/{idBook}")]
        public HttpResponseMessage DeleteBookFromWishList(int idUser, int idBook)
        {
            try
            {
                User user = userLinker.GetUser(idUser);
                Book book = bookLinker.GetBook(idBook);
                userLinker.DeleteBookFromWishList(user, idBook);
                return Request.CreateResponse(HttpStatusCode.OK, "Book from wish list deleted");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }
    }
}