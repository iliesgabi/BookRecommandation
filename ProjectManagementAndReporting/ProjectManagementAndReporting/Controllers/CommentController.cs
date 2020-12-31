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
    public class CommentController : ApiController
    {
        private readonly CommentLinker commentLinker = CommentLinker.Instance();

        [HttpGet]
        [Route("api/comment")]
        // GET api/comment
        public List<Comment> GetComments()
        {
            return commentLinker.GetComments();
        }

        [HttpGet]
        [Route("api/comment/id/{idComment}")]
        // GET api/comment/id/51
        public Comment GetComment(int idComment)
        {
            return commentLinker.GetComment(idComment);
        }

        [HttpGet]
        [Route("api/comment/author/{author}")]
        // GET api/comment/author/nume
        public List<Comment> GetCommentsByAuthor(string author)
        {
            return commentLinker.GetCommentsByAuthor(author);
        }

        [HttpGet]
        [Route("api/comment/date/{dateTime}")]
        // GET api/comment/date/2020-04-01
        public List<Comment> GetCommentsByDate(DateTime dateTime)
        {
            return commentLinker.GetCommentsByDate(dateTime);
        }


        [HttpPost]
        [Route("api/comment/add/{author}/{text}")]
        public HttpResponseMessage AddComment(string author, string text)
        {
            try
            {
                commentLinker.AddComment(author, text);
                return Request.CreateResponse(HttpStatusCode.OK, "Comment created");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }

        [HttpPut]
        [Route("api/comment/updateText/{id}/{text}")]
        public HttpResponseMessage UpdateComment(int id, string text)
        {
            try
            {
                Comment comment = commentLinker.GetComment(id);
                commentLinker.UpdateText(comment, text);
                return Request.CreateResponse(HttpStatusCode.OK, "Comment changed");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }

        [HttpDelete]
        [Route("api/comment/delete/{idComment}")]
        public HttpResponseMessage DeleteComment(int idComment)
        {
            try
            {
                Comment comment = commentLinker.GetComment(idComment);
                commentLinker.DeleteComment(comment);
                return Request.CreateResponse(HttpStatusCode.OK, "Comment deleted");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect input");
            }
        }

    }
}