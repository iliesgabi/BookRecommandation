﻿using System;
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

        [HttpPost]
        [Route("api/user/add/{id}/{isAdmin}/{username}/{password}")]
        public HttpResponseMessage AddUser(int id, int isAdmin, string username, string password)
        {
            try
            {
                userLinker.AddUser(id, isAdmin, username, password);
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
    }
}