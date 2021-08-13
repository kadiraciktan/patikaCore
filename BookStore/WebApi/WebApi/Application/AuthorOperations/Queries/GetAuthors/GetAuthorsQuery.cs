using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccess;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authors = _context.Authors.OrderBy(x => x.Id).ToList();
            return _mapper.Map<List<AuthorsViewModel>>(authors);
        }
    }

    public class AuthorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
