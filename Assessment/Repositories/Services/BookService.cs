using Assessment.Models;
using Assessment.Repositories.Interfaces;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Assessment.Repositories.Services
{
    public class BookService : IBookService
    {
        private BookContext _context;
        public BookService(BookContext context)
        {
            this._context = context;
        }
        public List<Books> GetBooksListByPublisherAuthorNameTitle()
        {
            List<Books> sortedBooks;
            try
            {
                sortedBooks = _context.BooksData.OrderBy(b => b.Publisher)
                                .ThenBy(b => b.AuthorLastName)
                                .ThenBy(b => b.AuthorFirstName)
                                .ThenBy(b => b.Title)
                                .ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return sortedBooks;
        }
        public List<Books> GetBooksListByAuthorNameTitle()
        {
            List<Books> sortedBooks;
            try
            {
                sortedBooks = _context.BooksData.OrderBy(b => b.AuthorLastName)
                                .ThenBy(b => b.AuthorFirstName)
                                .ThenBy(b => b.Title)
                                .ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return sortedBooks;
        }

        public List<Books> GetBooksListBySP_PublisherAuthorNameTitle()
        {
            List<Books> sortedBooks;
            try
            {
                sortedBooks = _context.BooksData.FromSqlRaw("EXEC GetBooksByPublisherAuthorTitle").ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return sortedBooks;
        }

        public List<Books> GetBooksListBySP_AuthorNameTitle()
        {
            List<Books> sortedBooks;
            try
            {
                sortedBooks = _context.BooksData.FromSqlRaw("EXEC GetBooksByAuthorTitle").ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return sortedBooks;
        }
        public Books GetBookDetailsById(int bookId)
        {
            Books? bk;
            try
            {
                bk = _context.Find<Books>(bookId);
            }
            catch (Exception)
            {
                throw;
            }
            return bk;
        }
        public decimal GetAllBooksTotalPriceAsync()
        {
            decimal totalprice;
            try
            {
                totalprice = _context.BooksData.Sum(b => b.Price);
            }
            catch (Exception)
            {
                throw;
            }
            return totalprice;
        }
        public ResultModel SaveBooks(Books _booksModel)
        {
            ResultModel model = new ResultModel();
            try
            {
                Books _temp = GetBookDetailsById(_booksModel.Id);
                if (_temp != null)
                {
                    _temp.Publisher = _booksModel.Publisher;
                    _temp.Title = _booksModel.Title;
                    _temp.AuthorFirstName = _booksModel.AuthorFirstName;
                    _temp.AuthorLastName = _booksModel.AuthorLastName;
                    _temp.Price = _booksModel.Price;
                    _temp.PublishYear = _booksModel.PublishYear;
                    _context.Update<Books>(_temp);
                    model.StatusMessage = "Books Details Update Successfully";
                }
                else
                {
                    _context.Add<Books>(_booksModel);
                    model.StatusMessage = "Books Details Inserted Successfully";
                }
                _context.SaveChanges();
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.StatusMessage = "Error : " + ex.Message;
            }
            return model;
        }

        public ResultModel SaveBulkBooksData(List<Books> _bookList)
        {
            ResultModel model = new ResultModel();
            try
            {
                _context.BulkInsertAsync<Books>(_bookList);
                _context.SaveChanges();
                model.IsSuccess = true;
                model.StatusMessage = "Multiple Books Details Inserted Successfully";
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.StatusMessage = "Error : " + ex.Message;
            }
            return model;
        }
    }
}
