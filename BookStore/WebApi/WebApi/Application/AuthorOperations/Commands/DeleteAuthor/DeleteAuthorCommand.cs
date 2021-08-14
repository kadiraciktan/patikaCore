using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccess;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookContext _context;
        public DeleteAuthorCommand(IBookContext context)
        {
            _context = context;
        }

        public void Handle()
        {

            var author = _context.Authors.FirstOrDefault(x => x.Id == AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("Yazar Bulunamadı");
            }

            if (_context.Books.Any(x=>x.AuthorId==AuthorId))
            {
                throw new InvalidOperationException("Bu Yazara Ait Kitap Mevcut. Yazar Silinemez");
            }
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}
