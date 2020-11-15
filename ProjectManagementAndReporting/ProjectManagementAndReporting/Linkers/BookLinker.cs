using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectManagementAndReporting.Database;
using ProjectManagementAndReporting.Models;

namespace ProjectManagementAndReporting.Linkers
{
    public sealed class BookLinker
    {
        private static readonly BookLinker bookLinker = new BookLinker();
        private readonly DbContext dataBase;

        private BookLinker()
        {
            this.dataBase = new Database.DbContext();
        }

        public static BookLinker Instance()
        {
            return bookLinker;
        }

        public List<Book> GetBooks()
        {
            return dataBase.Data.Books;
        }

        public Book GetBook(int id)
        {
            return dataBase.Data.Books.Find(book => book.Id == id);
        }

        public List<Book> GetBooks(string stringSearch)
        {
            return dataBase.Data.Books.FindAll(book => (book.Title.ToLower().Contains(stringSearch.ToLower()) || book.Author.ToLower().Contains(stringSearch.ToLower())));
        }

        public List<Book> GetBooksByAuthor(string author)
        {
            return dataBase.Data.Books.FindAll(book => book.Author.ToLower().Contains(author.ToLower()));
        }

        public List<Book> GetBooksByTitle(string title)
        {
            return dataBase.Data.Books.FindAll(book => book.Title.ToLower().Contains(title.ToLower()));
        }

        public List<Book> GetBooksByPublishingHouse(string publishingHouse)
        {
            return dataBase.Data.Books.FindAll(book => book.PublishingHouse.ToLower().Contains(publishingHouse.ToLower()));
        }

        public List<Book> GetBooksByRating(float rating)
        {
            return dataBase.Data.Books.FindAll(book => (book.Rating == rating));
        }

        public void AddBook(int id, string title, string author, int year, string publishingHouse)
        {
            Book book = new Book();
            book.Id = id;
            book.Title = title;
            book.Author = author;
            book.Year = year;
            book.PublishingHouse = publishingHouse;

            dataBase.Data.Books.Add(book);
            dataBase.Save();
        }
        public void UpdateBookTitle(Book book, string title)
        {
            book.Title = title;
            dataBase.Save();
        }

        public void UpdateBookAuthor(Book book, string author)
        {
            book.Author = author;
            dataBase.Save();
        }

        public void UpdateBookYear(Book book, int year)
        {
            book.Year = year;
            dataBase.Save();
        }

        public void UpdateBookPublishingHouse(Book book, string publishingHouse)
        {
            book.PublishingHouse = publishingHouse;
            dataBase.Save();
        }
       
        public void AddCommentToBook(Book book, int idComment)
        {
            Comment comment = dataBase.Data.Comments.Find(com => com.Id == idComment);
            book.Comments.Add(comment);
            dataBase.Save();
        }
       
        public void DeleteBook(Book book)
        {
            dataBase.Data.Books.Remove(book);
            dataBase.Save();

        }
        
        public Comment GetCommentFromBook(Book book, int idComment)
        {
            return book.Comments.Find(comment => comment.Id == idComment);
        }

        public void DeleteBookComment(Book book, Comment comment)
        {
            book.Comments.Remove(comment);
            dataBase.Save();
        } 
    }
}