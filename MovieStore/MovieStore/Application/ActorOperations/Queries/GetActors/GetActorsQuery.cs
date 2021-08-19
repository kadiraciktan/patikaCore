using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DataAccess;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Application.ActorOperations.Queries.GetActors
{
    public class GetActorsQuery
    {
        private readonly IMovieContext _context;
        private readonly IMapper _mapper;

        public GetActorsQuery(IMovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ActorsViewModel> Handle()
        {
            var actorList = _context.Actors
                //.Include(x => x.MovieActors)
                //.ThenInclude(x => x.Movie)
                //.ThenInclude(x => x.Director)
                //.Include(x => x.MovieActors)
                //.ThenInclude(x => x.Movie)
                //.ThenInclude(x => x.Genre)
                .ToList();
                        
            var list =  _mapper.Map<List<ActorsViewModel>>(actorList);
            return list;
        }

    }

    public class ActorsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
      //  public List<MovieActor> MovieActors { get; set; }
    }

}
