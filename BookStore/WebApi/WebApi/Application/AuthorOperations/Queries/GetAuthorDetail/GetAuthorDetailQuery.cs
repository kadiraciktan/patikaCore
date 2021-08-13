using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccess;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;
        public int AuthorId;

        public GetAuthorDetailQuery(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _context.Authors.FirstOrDefault(x=>x.Id==AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("Yazar Bulunamadı");
            }

            return _mapper.Map<AuthorDetailViewModel>(author);
        }           

    }


    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
