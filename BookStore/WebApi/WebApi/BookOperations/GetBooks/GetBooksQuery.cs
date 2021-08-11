using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DataAccess;
using WebApi.Entities;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookContext _context;
        public GetBooksQuery(BookContext context)
        {
            _context = context;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList =  _context.Books.OrderBy(x => x.Id).ToList();
            List<BooksViewModel> booksViewModel= new List<BooksViewModel>();
            foreach (var book in bookList)
            {
                booksViewModel.Add(new BooksViewModel()
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
                    PageCount = book.PageCount

                });       
            }

            return booksViewModel;
        }






    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string  Genre { get; set; }

    }
}
