using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectManagementAndReporting.Database;
using ProjectManagementAndReporting.Models;

namespace ProjectManagementAndReporting.Linkers
{
    public sealed class CommentLinker
    {
        private readonly static CommentLinker commentLinker = new CommentLinker();
        private readonly DbContext dataBase;

        private CommentLinker()
        {
            this.dataBase = new Database.DbContext();
        }

        public static CommentLinker Instance()
        {
            return commentLinker;
        }
        
        public List<Comment> GetComments()
        {
            return dataBase.Data.Comments;
        }

        public Comment GetComment(int idComment)
        {
            return dataBase.Data.Comments.Find(comment => comment.Id == idComment);
        }

        public List<Comment> GetCommentsByAuthor(string author)
        {
            return dataBase.Data.Comments.FindAll(comment => comment.Author.ToLower().Contains(author.ToLower()));
        }

        public List<Comment> GetCommentsByDate(DateTime date)
        {
            Console.WriteLine(date.Date);
            return dataBase.Data.Comments.FindAll(comment => comment.Date.Date.Equals(date.Date));
        }
    
        public void AddComment(string author, string text)
        {
            Comment comment = new Comment();
            comment.Id = dataBase.Data.IdComment;
            dataBase.Data.IdComment = dataBase.Data.Comments.Count;
            comment.Author = author;
            comment.Text = text;
            comment.Date = DateTime.Now;

            dataBase.Data.Comments.Add(comment);
            dataBase.Save();
        }

        public void UpdateText(Comment comment, string newText)
        {
            comment.Text = newText;
            dataBase.Save();
        }

        public void DeleteComment(Comment comment)
        {
            dataBase.Data.Comments.Remove(comment);
            dataBase.Save();
        }
    }
}