using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.BookOperations.BookDetail;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DataAccess;
using WebApi.Entities;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookContext _context;
        public BookController(BookContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            return Ok(query.Handle());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId = id;
                return Ok(query.Handle());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }


        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.BookId= id;
                command.Model = updatedBook;
                command.Handle();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handle();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //[HttpGet]
        //public Book Get([FromQuery]string id)
        //{
        //    return BookList.FirstOrDefault(x => x.Id == int.Parse(id));
        //}

    }
}


