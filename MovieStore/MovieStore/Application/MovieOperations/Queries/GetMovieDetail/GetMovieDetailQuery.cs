
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.DirectorOperations.Queries.GetDirectors;
using MovieStore.DataAccess;
using MovieStore.Entities;

namespace MovieStore.Application.MovieOperations.Queries.GetMovieDetail;
public class GetMovieDetailQuery
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;
    public int MovieId { get; set; }

    public GetMovieDetailQuery(IMovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public MovieDetailModel Handle()
    {
        var movie = _context.Movies
            .Include(x => x.Director)
            .Include(x => x.Genre)
            .Include(x => x.MovieActors)
            .ThenInclude(x => x.Actor)
            .FirstOrDefault(x => x.Id == MovieId);
        if (movie is null)
        {
            throw new InvalidOperationException("Film Bulunamadı");
        }

        var result = _mapper.Map<MovieDetailModel>(movie);

        return result;

    }
}

public class MovieDetailModel
{
    public string Id { get; set; }

    public string Name { get; set; }

    public DateTime Year { get; set; }

    public Genre Genre { get; set; }

    public DirectorsViewModel Director { get; set; }

    public List<GetMovieDetailActorsModel> Actors { get; set; }

    public float Price { get; set; }
}

public class GetMovieDetailActorsModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}
