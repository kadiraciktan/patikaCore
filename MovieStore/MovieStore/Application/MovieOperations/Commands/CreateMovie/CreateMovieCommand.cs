
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

        //foreach (var item in Model.Actors)
        //{
        //    if (!_context.Actors.Any(x=>x.Id==item.ActorId))
        //    {
        //        throw new InvalidOperationException($" {item.Actor.Name} Aktör Bulunamadı");
        //    }
        //}

        
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
