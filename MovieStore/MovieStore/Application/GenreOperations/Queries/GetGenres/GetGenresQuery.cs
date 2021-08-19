using AutoMapper;
using MovieStore.DataAccess;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IMovieContext _context;
        private readonly IMapper _mapper;

        public GetGenresQuery(IMovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genreList =  _context.Genres.Where(x=>x.IsActive).OrderBy(x=>x.Id).ToList();
            List<GenresViewModel> genresViewModels = _mapper.Map<List<GenresViewModel>>(genreList);
            return genresViewModels;
        }
    }

    public class GenresViewModel
    {
        public int Id { get;set;  }
        public string Name {  get;set; }
    }
}
