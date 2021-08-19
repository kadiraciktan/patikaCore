
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.ActorOperations.Queries.GetActors;
using MovieStore.Application.DirectorOperations.Queries.GetDirectors;
using MovieStore.DataAccess;
using MovieStore.Entities;

namespace MovieStore.Application.MovieOperations.Queries.GetMovies;
public class GetMoviesQuery
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;

    public GetMoviesQuery(IMovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<MoviesViewModel> Handle()
    {
        var list = _context.Movies
            .Include(x=>x.Director)
            .Include(x=>x.Genre)
            .Include(x=>x.MovieActors)
            .ThenInclude(x=>x.Actor)
            .ToList();
        var result = _mapper.Map<List<MoviesViewModel>>(list);
        return result;
    }
}

public class MoviesViewModel
{
    public string Id {  get; set; }

    public string Name { get; set; }

    public DateTime Year { get; set; }

    public Genre Genre { get; set; }

    public DirectorsViewModel Director { get; set; }

    public virtual ICollection<ActorsViewModel> Actors { get; set; }

    public float Price { get; set; }
}
