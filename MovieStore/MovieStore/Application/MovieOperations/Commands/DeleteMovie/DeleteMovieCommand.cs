
using AutoMapper;
using MovieStore.DataAccess;

namespace MovieStore.Application.MovieOperations.Commands.DeleteMovie;
public class DeleteMovieCommand
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;
    public int MovieId {  get; set; }
    public DeleteMovieCommand(IMovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var movie = _context.Movies.FirstOrDefault(x=>x.Id==MovieId);
        if (movie is null)
        {
            throw new InvalidOperationException("Film Bulunamadı");
        }
        _context.Movies.Remove(movie);
        _context.SaveChanges();

    }


}
