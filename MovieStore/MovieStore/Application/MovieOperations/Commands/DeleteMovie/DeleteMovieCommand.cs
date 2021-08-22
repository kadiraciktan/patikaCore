
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

        var order = _context.Orders.FirstOrDefault(x => x.MovieId == MovieId);
        if (order is not null)
        {
            throw new InvalidOperationException("Film Silinemez Satın Alım Mevcut");
        }
        movie.IsActive = false;
        _context.SaveChanges();

    }


}
