using System;
using System.Collections.Generic;
using System.Linq;
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

        public Dictionary<int, int> GetBooksRatings(int idBook)
        {
            return dataBase.Data.Books.Find(book => book.Id == idBook).Ratings;
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

        public void AddBook(string title, string author, int year, string publishingHouse, string description)
        {
            Book book = new Book();
            book.Id = dataBase.Data.IdBook;
            dataBase.Data.IdBook = dataBase.Data.Books.Count;
            book.Title = title;
            book.Author = author;
            book.Year = year;
            book.PublishingHouse = publishingHouse;
            book.ShortDescription = description;

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

        public void AddDescriptionToBook(Book book, string description)
        {
            book.ShortDescription = description;
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

        public void AddRatingToBook(int idUser, Book book, int rating)
        {
            if (book.Ratings.ContainsKey(idUser))
            {
                book.Ratings[idUser] = rating;
            } else
                book.Ratings.Add(idUser, rating);

            int sum = 0;
            List<int> raitings = book.Ratings.Values.ToList();
            for (int i = 0; i < raitings.Count; i++)
                sum += raitings.ElementAt(i);

            book.Rating = (float)sum / raitings.Count;

            dataBase.Save();
        }

        public int[,] GetLearnSetMatrix()
        {
            dataBase.GetData();
            List<User> users = dataBase.Data.Users;
            List<Book> books = dataBase.Data.Books;

            int[,] R = new int[books.Count(), users.Count()];

            for(int i = 0; i < books.Count(); i ++)
            {
                List<int> userBook = books.ElementAt(i).Ratings.Keys.ToList();
                for(int j = 0; j < users.Count(); j++)
                {
                    if (userBook.Contains(j))
                        books.ElementAt(i).Ratings.TryGetValue(j, out R[i, j]);
                    else
                        R[i, j] = 0;
                }
            }

            return R;
        }


        private double dotProduct(double[] P, double[] Q)
        {
            double prod = 0.0;
            for (int i = 0; i < P.Length; i++) 
                prod = prod + (P[i] * Q[i]);
            return prod;
        }

        private double[] GetRow(double[,] vec, int row, int len)
        {
            double[] q = new double[len]; 
            for (int i = 0; i < len; i++)
                q[i] = vec[row, i];
            return q;
        }

        private double[] GetColumn(double[,] vec, int col, int len)
        {
            double[] q = new double[len];
            for (int i = 0; i < len; i++)
                q[i] = vec[i, col];
            return q;
        }

        public double[,] MultiplyMatrix(double[,] A, double[,] B)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);
            int rB = B.GetLength(0);
            int cB = B.GetLength(1);
            double temp = 0;
            double[,] kHasil = new double[rA, cB];
            if (cA != rB)
            {
                Console.WriteLine("matrik can't be multiplied !!");
                return null;
            }
            else
            {
                for (int i = 0; i < rA; i++)
                {
                    for (int j = 0; j < cB; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < cA; k++)
                        {
                            temp += A[i, k] * B[k, j];
                        }
                        kHasil[i, j] = temp;
                    }
                }
                return kHasil;
            }
        }

        private int PosMaximum(double[] vec1, int[] vec2, int len)
        {
            int pos = 0;
            double max = -99999;
            for(int i = 0; i < len; i++)
                if (max < vec1[i] && vec2[i] == 0)
                {
                    max = vec1[i];
                    pos = i;
                }
            return pos;
        }

        public double[,] GetRecommandation()
        {
            int[,] learnSet = GetLearnSetMatrix();

            List<User> users = dataBase.Data.Users;
            List<Book> books = dataBase.Data.Books;
            int usersLen = users.Count();
            int booksLen = books.Count();

            int latentFactors = 2;
            double gamma = 0.0002;
            double lambda = 0.02;

            double[,] R = new double[booksLen, usersLen];
            double[,] P = new double[booksLen, latentFactors];
            double[,] Q = new double[latentFactors, usersLen];

            Random r = new Random();

            for (int i = 0; i < booksLen; i++)
                for (int j = 0; j < latentFactors; j++)
                    P[i, j] = r.NextDouble() * 5 ;

            for (int i = 0; i < latentFactors; i++)
                for (int j = 0; j < usersLen; j++)
                    Q[i, j] = r.NextDouble() * 5;

            int steps = 5000;

            for(int repeat = 0; repeat < steps; repeat++)
            {
                for (int i = 0; i < booksLen; i++)
                {
                    for (int j = 0; j < usersLen; j++)
                    {
                        if (learnSet[i, j] > 0.0d)
                        {
                            double Rij = learnSet[i, j];
                            double err = Rij - dotProduct(GetColumn(Q, j, latentFactors), GetRow(P, i, latentFactors));

                            for (int f = 0; f < latentFactors; f++)
                            {
                                P[i, f] = P[i, f] + gamma * (2 * err * Q[f, j] - lambda * P[i, f]);
                                Q[f, j] = Q[f, j] + gamma * (2 * err * Q[f, j] - lambda * P[i, f]);
                            }
                        }
                    }
                }

                R = MultiplyMatrix(P, Q);
                double e = 0.0;
                for(int i = 0; i < booksLen; i++)
                {
                    for(int j = 0; j < usersLen; j++)
                    {
                        if (R[i,j] > 0)
                        {
                            e = e + (R[i, j] - dotProduct(GetColumn(Q, j, latentFactors), GetRow(P, i, latentFactors))) * (R[i, j] - dotProduct(GetColumn(Q, j, latentFactors), GetRow(P, i, latentFactors)));
                            for (int k = 0; k < latentFactors; k++)
                            {
                                e = e + (gamma / 2) * (P[i, k] * P[i, k]) + (Q[k, j] * Q[k, j]);
                            }
                        }
                    }
                }
                if (e < 0.001)
                    break;
            }
            return R;
        }

        public List<int> GetRecommandationForUser(int idUser)
        {
            double[,] R = GetRecommandation();

            List<User> users = dataBase.Data.Users;
            List<Book> books = dataBase.Data.Books;

            int usersLen = users.Count();
            int booksLen = books.Count();

            double[] aux = new double[booksLen];
            int[] recommandation = new int[booksLen];

            aux = GetColumn(R, idUser, booksLen);
            int[] auxPos = new int[booksLen];
            for (int i = 0; i < booksLen; i++)
                auxPos[i] = 0;

            for (int i = 0; i < aux.Length; i++)
            {
                int pos = PosMaximum(aux, auxPos, booksLen);
                recommandation[i] = pos;
                auxPos[pos] = 1;
            }

            UserLinker userLinker = UserLinker.Instance();
            User user = userLinker.GetUser(idUser);
            
            return recommandation.ToList();
        }
    }
}