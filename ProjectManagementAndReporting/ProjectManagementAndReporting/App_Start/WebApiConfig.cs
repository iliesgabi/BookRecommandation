using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using Bogus;
using Newtonsoft.Json;
using ProjectManagementAndReporting.Models;
using Swashbuckle.Swagger.XmlComments;

namespace ProjectManagementAndReporting
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            GenerateMyData();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void GenerateMyData()
        {
            var commentsId = 0;
            var comments = new Faker<Comment>()
                .StrictMode(true)
                .RuleFor(c => c.Id, f => commentsId++)
                .RuleFor(c => c.Text, f => f.Lorem.Sentence(10))
                .RuleFor(c => c.Author, f => (f.Person.FirstName + " " + f.Person.LastName))
                .RuleFor(c => c.Date, f => f.Date.Past(1));


            var bookId = 0;
            var books = new Faker<Book>()
                .StrictMode(true)
                .RuleFor(b => b.Id, f => bookId++)
                .RuleFor(b => b.Title, f => f.Hacker.Phrase())
                .RuleFor(b => b.Author, f => (f.Person.FirstName + " " + f.Person.LastName))
                .RuleFor(b => b.Year, f => f.Random.Number(1900, 2020))
                .RuleFor(b => b.PublishingHouse, f => f.Lorem.Sentence(1))
                .RuleFor(b => b.ShortDescription, f => f.Lorem.Sentence(10))
                .RuleFor(b => b.Rating, f => f.Random.Float())
                .RuleFor(b => b.Comments, f => comments.Generate(5));


            var userId = 0;
            var users = new Faker<User>()
                .StrictMode(true)
                .RuleFor(e => e.Id, f => userId++)
                .RuleFor(e => e.IsAdmin, f => f.Random.Number(0, 1))
                .RuleFor(e => e.Username, f => f.Lorem.Sentence(1))
                .RuleFor(e => e.Password, f => f.Lorem.Sentence(1))
                .RuleFor(e => e.ReadBooks, f => books.Generate(3))
                .RuleFor(e => e.ActiveBooks, f => books.Generate(2))
                .RuleFor(e => e.WishList, f => books.Generate(2));


            var allBooks = JsonConvert.SerializeObject(books.Generate(10));
            File.WriteAllText("C:\\allBooks.txt", allBooks);

            var allComments = JsonConvert.SerializeObject(comments.Generate(20));
            File.WriteAllText("C:\\allComments.txt", allComments);

            var allUsers = JsonConvert.SerializeObject(users.Generate(6));
            File.WriteAllText("C:\\allUsers.txt", allUsers);
        }
    }
}
