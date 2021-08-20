
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DataAccess;
using MovieStore.Entities;

namespace MovieStore.Application.MovieOperations.Commands.UpdateMovie;
public class UpdateMovieCommand
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;

    public UpdateMovieModel Model { get; set; }
    public int MovieId { get; set; }

    public UpdateMovieCommand(IMovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
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

        if (!_context.Genres.Any(x=>x.Id==movie.GenreId))
        {
            throw new InvalidOperationException("Kategori Bulunamadı");

        }

        if (!_context.Directors.Any(x => x.Id == movie.DirectorId))
        {
            throw new InvalidOperationException("Yönetmen Bulunamadı");

        }



        foreach (var item in Model.MovieActors)
        {
            if (!_context.Actors.Any(x => x.Id == item))
            {
                throw new InvalidOperationException($" {item} Id li Aktör Bulunamadı");
            }
        }


        var result = _mapper.Map<Movie>(Model);
        movie.Name = movie.Name == default ? movie.Name : result.Name;
        movie.Year = movie.Year == default ? movie.Year : result.Year;
        movie.GenreId = movie.GenreId == default ? movie.GenreId : result.GenreId;
        movie.DirectorId = movie.DirectorId == default ? movie.DirectorId : result.DirectorId;
        movie.Price = movie.Price == default ? movie.Price : result.Price;
        movie.MovieActors = result.MovieActors.Count>0 ? result.MovieActors : movie.MovieActors;
        _context.SaveChanges();

    }
}

public class UpdateMovieModel
{
    public string Name { get; set; }

    public DateTime Year { get; set; }

    public int GenreId { get; set; }

    public int DirectorId { get; set; }

    public float Price { get; set; }

    public virtual List<int> MovieActors { get; set; }
}
public class UpdateMovieModelActor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}
