using CRUDtesting.Data;
using CRUDtesting.Models;
using CRUDtesting.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CRUDtesting.Controllers
{
    // localhost:xxxx/api/books
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public BooksController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var allBooks = dbContext.Books.ToList();
            return allBooks.Count == 0 ? NotFound("No books found") : Ok(allBooks);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetBookById(Guid id)
        {
            var book = dbContext.Books.Find(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook(AddBookDto addBookDto)
        {
            var bookEntity = new Book()
            {
                Name = addBookDto.Name,
                Price = addBookDto.Price,
                Quantity = addBookDto.Quantity
            };

            dbContext.Books.Add(bookEntity); 
            dbContext.SaveChanges();

            return Ok(bookEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateBook(Guid id, UpdateBookDto updateBookDto)
        {
            var book = dbContext.Books.Find(id);

            if(book == null)return NotFound();
            book.Name = updateBookDto.Name;
            book.Price = updateBookDto.Price;
            book.Quantity = updateBookDto.Quantity;

            dbContext.SaveChanges();
            return Ok(book);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteBook(Guid id)
        {
            var book = dbContext.Books.Find(id);
            if(book == null) return NotFound();
            dbContext.Books.Remove(book);
            dbContext.SaveChanges();
            return Ok(book);    
        }
    }
}
