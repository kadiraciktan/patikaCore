using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DataAccess;
using WebApi.Entities;

namespace WebApi.BookOperations.BookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookContext _context;
        public int BookId { get; set; }
        public GetBookDetailQuery(BookContext context)
        {
            _context = context;
        }

        public BookDetailViewModel Handle()
        {
            Book book = _context.Books.FirstOrDefault(x => x.Id == BookId);

            if (book == null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }

            BookDetailViewModel view = new BookDetailViewModel();
            view.Title = book.Title;
            view.Genre = ((GenreEnum)book.GenreId).ToString();
            view.PageCount =   book.PageCount;
            view.PublishDate = book.PublishDate.ToString("dd/MM/yyyy");
            return view;
        }
        
    }
    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
