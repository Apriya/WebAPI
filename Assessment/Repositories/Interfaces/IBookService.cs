using Assessment.Models;

namespace Assessment.Repositories.Interfaces
{
    public interface IBookService
    {
        List<Books> GetBooksListByPublisherAuthorNameTitle();
        List<Books> GetBooksListByAuthorNameTitle();
        Books GetBookDetailsById(int bookId);
        List<Books> GetBooksListBySP_PublisherAuthorNameTitle();
        List<Books> GetBooksListBySP_AuthorNameTitle();
        decimal GetAllBooksTotalPriceAsync();
        ResultModel SaveBooks(Books _books);
        ResultModel SaveBulkBooksData(List<Books> _bookList);
    }
}
