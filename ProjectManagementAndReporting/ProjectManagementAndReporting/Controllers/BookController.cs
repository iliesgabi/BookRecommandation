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
    public class BookController : ApiController
    {
        private readonly BookLinker bookLinker = BookLinker.Instance();

        [HttpGet]
        [Route("api/book")]
        public List<Book> GetBooks()
        {
            return bookLinker.GetBooks();
        }
        
        [HttpGet]
        [Route("api/book/id/{idBook}")]
        // GET api/book/id/5
        public Book GetBook(int idBook)
        {
            return bookLinker.GetBook(idBook);
        }

        [HttpGet]
        [Route("api/book/search/{stringSearch}")]
        //Get api/book/search/string
        public List<Book> GetBooks(string stringSearch)
        {
            return bookLinker.GetBooks(stringSearch);
        }

        [HttpGet]
        [Route("api/book/search/author/{author}")]
        //Get api/book/search/author/string
        public List<Book> GetBooksByAuthor(string author)
        {
            return bookLinker.GetBooksByAuthor(author);
        }

        [HttpGet]
        [Route("api/book/search/title/{title}")]
        //Get api/book/search/title/string
        public List<Book> GetBooksByTitle(string title)
        {
            return bookLinker.GetBooksByTitle(title);
        }

        [HttpGet]
        [Route("api/book/search/publishingHouse/{publishingHouse}")]
        //Get api/book/search/publishingHouse/string
        public List<Book> GetBooksByPublishingHouse(string publishingHouse)
        {
            return bookLinker.GetBooksByPublishingHouse(publishingHouse);
        }

        [HttpPost]
        [Route("api/book/add/{id}/{title}/{author}/{year}/{publishingHouse}")]
        public HttpResponseMessage AddBook(int id, string title, string author, int year, string publishingHouse)
        {
            try
            {
                bookLinker.AddBook(id, title, author, year, publishingHouse);
                return Request.CreateResponse(HttpStatusCode.OK, "Book created");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }

        [HttpPut]
        [Route("api/book/updateTitle/{id}/{title}")]
        public HttpResponseMessage UpdateBookTitle(int id, string title)
        {
            try
            {
                Book book = bookLinker.GetBook(id);
                bookLinker.UpdateBookTitle(book, title);
                return Request.CreateResponse(HttpStatusCode.OK, "Book changed");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }

        [HttpPut]
        [Route("api/book/updateAuthor/{id}/{author}")]
        public HttpResponseMessage UpdateBookAuthor(int id, string author)
        {
            try
            {
                Book book = bookLinker.GetBook(id);
                bookLinker.UpdateBookAuthor(book, author);
                return Request.CreateResponse(HttpStatusCode.OK, "Book changed");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }

        [HttpPut]
        [Route("api/book/updateYear/{id}/{year}")]
        public HttpResponseMessage UpdateBookYear(int id, int year)
        {
            try
            {
                Book book = bookLinker.GetBook(id);
                bookLinker.UpdateBookYear(book, year);
                return Request.CreateResponse(HttpStatusCode.OK, "Book changed");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }

        [HttpPut]
        [Route("api/book/updatePH/{id}/{publishingHouse}")]
        public HttpResponseMessage UpdateBookPublishingHouse(int id, string publishingHouse)
        {
            try
            {
                Book book = bookLinker.GetBook(id);
                bookLinker.UpdateBookPublishingHouse(book, publishingHouse);
                return Request.CreateResponse(HttpStatusCode.OK, "Book changed");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }

        [HttpPut]
        [Route("api/book/addComment/{idBook}/{idComment}")]
        public HttpResponseMessage AddCommentToBook(int idBook, int idComment)
        {
            try
            {
                Book book = bookLinker.GetBook(idBook);
                bookLinker.AddCommentToBook(book, idComment);
                return Request.CreateResponse(HttpStatusCode.OK, "Comment added");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }

        [HttpPut]
        [Route("api/book/addDescription/{idBook}/{description}")]
        public HttpResponseMessage AddShortDescriptionToBook(int idBook, string description)
        {
            try
            {
                Book book = bookLinker.GetBook(idBook);
                bookLinker.AddDescriptionToBook(book, description);
                return Request.CreateResponse(HttpStatusCode.OK, "Description added");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }

        [HttpDelete]
        [Route("api/book/delete/{idBook}")]
        public HttpResponseMessage DeleteBook(int idBook)
        {
            try
            {
                Book book = bookLinker.GetBook(idBook);
                bookLinker.DeleteBook(book);
                return Request.CreateResponse(HttpStatusCode.OK, "Book deleted");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }

        [HttpDelete]
        [Route("api/book/deleteComment/{idBook}/{idComment}")]
        public HttpResponseMessage DeleteBookComment(int idBook, int idComment)
        {
            try
            {
                Book book = bookLinker.GetBook(idBook);
                Comment comment = bookLinker.GetCommentFromBook(book, idComment);
                bookLinker.DeleteBookComment(book, comment);
                return Request.CreateResponse(HttpStatusCode.OK, "Comment deleted");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }
    }
}
