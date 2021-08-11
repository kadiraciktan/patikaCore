using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccess;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        private readonly BookContext _context;

        public UpdateBookCommand(BookContext bookContext)
        {
            _context = bookContext;
        }
        public void Handle()
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == Model.Id);
            if (book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }

            //Eğer değişmiş bir değer gelirse değiştirir
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            _context.SaveChanges();
        }

    }

    public class UpdateBookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
