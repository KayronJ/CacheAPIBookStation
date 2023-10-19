using BookStation.Models;
using BookStation.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        public BookController() 
        {
        
        }

        // Get all books
        [HttpGet]
        public ActionResult <List<Book>> GetAll() =>
            BookService.GetAll();

        // Get a specific book by id
        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var book = BookService.Get(id);
            if(book == null)
                return NotFound();

            return book;
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            // This code will save the book and return a result
            BookService.Add(book);

            //CreateAtAction : The book was added on memory cache.
            //The book is included on response body at media type,
            //as defined in the HTTP request header accept(JSON by default).
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Book book) 
        {
            // This code will update the book and return a result
            if (id != book.Id)
                //BadRequest : The object book of request body is invalid.
                return BadRequest();

            var existingBook = BookService.Get(id);
            if (existingBook is null)

                //NotFound : A Book that correspond to the parameter id provided doesn't exist on memory cache
                return NotFound();

            BookService.Update(book);

            //NoContent : The book was updated on cache at memory.
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // This code will delete the book and return a result
            var book = BookService.Get(id);

            if(book is null)
            return NotFound();

            BookService.Delete(id);

            // The Book was excluded of the memory cache
            return NoContent();
        }

    }
}
