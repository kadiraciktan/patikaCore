
using AutoMapper;
using MovieStore.Application.ActorOperations.Commands.CreateActor;
using MovieStore.Application.ActorOperations.Queries.GetActors;
using MovieStore.DataAccess;
using MovieStore.Entities;

namespace MovieStore.Application.MovieOperations.Commands.CreateMovie;
public class CreateMovieCommand
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;
    public CreateMovieModel Model { get; set; }

    public CreateMovieCommand(IMovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var movie = _context.Movies.FirstOrDefault(x => x.Name == Model.Name);
        if (movie is not null)
        {
            throw new InvalidOperationException("Bu Film Zaten Mevcut.");
        }

        if (!_context.Genres.Any(x => x.Id == movie.GenreId))
        {
            throw new InvalidOperationException("Kategori Bulunamadı");

        }

        if (!_context.Directors.Any(x => x.Id == movie.DirectorId))
        {
            throw new InvalidOperationException("Yönetmen Bulunamadı");

        }


       foreach (var item in Model.MovieActors)
       {
           if (!_context.Actors.Any(x=>x.Id==item))
           {
               throw new InvalidOperationException($" {item} Id li Aktör Bulunamadı");
           }
       }


        var result = _mapper.Map<Movie>(Model);
        _context.Movies.Add(result);
        _context.SaveChanges();

    }


}
public class CreateMovieModel
{

    public string Name { get; set; }

    public DateTime Year { get; set; }

    public int GenreId { get; set; }

    public int DirectorId { get; set; }

    public float Price { get; set; }


    public virtual List<int> MovieActors {  get; set; }
}

public class CreateMovieModelActor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}
