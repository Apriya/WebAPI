using Assessment.Models;
using Assessment.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            this._bookService = bookService;
        }

        [HttpGet("get_by_publisher_author_title")]
        public async Task<IActionResult> GetBooksAsync()
        {
            List<Books> books = new List<Books>();
            books = _bookService.GetBooksListByPublisherAuthorNameTitle();
            return Ok(books);
        }

        [HttpGet("get_by_author_title")]
        public async Task<IActionResult> GetBooksByAuthorAsync()
        {
            List<Books> books = new List<Books>();
            books = _bookService.GetBooksListByAuthorNameTitle();
            return Ok(books);
        }

        [HttpGet("get_by_sp_publisher_author_title")]
        public async Task<IActionResult> GetBooksBySP_PublisherAuthorAsync()
        {
            List<Books> books = new List<Books>();
            books = _bookService.GetBooksListBySP_PublisherAuthorNameTitle();
            return Ok(books);
        }

        [HttpGet("get_by_sp_author_title")]
        public async Task<IActionResult> GetBooksBySP_AuthorAsync()
        {
            List<Books> books = new List<Books>();
            books = _bookService.GetBooksListBySP_AuthorNameTitle();
            return Ok(books);
        }

        [HttpGet("get_all_books_total_price")]
        public async Task<IActionResult> GetAllBooksTotalPriceAsync()
        {
            decimal totalPrice = _bookService.GetAllBooksTotalPriceAsync();
            return Ok(totalPrice);
        }

        [HttpPost("add_update_books_details")]
        public async Task<IActionResult> AddBooksAsync([FromBody] Books _bookdata)
        {
            ResultModel resultModel = new ResultModel();
            resultModel = _bookService.SaveBooks(_bookdata);
            return Ok(resultModel);
        }

        [HttpPost("add_multiple_books_details")]
        public async Task<IActionResult> AddMultipleBooksAsync([FromBody] List<Books> _bookList)
        {
            ResultModel resultModel = new ResultModel();
            resultModel = _bookService.SaveBulkBooksData(_bookList);
            return Ok(resultModel);
        }


    }
}
