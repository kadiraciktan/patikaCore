
using AutoMapper;
using MovieStore.DataAccess;

namespace MovieStore.Application.OrderOperations.Commands.UpdateOrder;
public class UpdateOrderCommand
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;
    public UpdateOrderModel Model { get; set; }
    public int OrderId { get; set; }

    public UpdateOrderCommand(IMovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var order = _context.Orders.FirstOrDefault(x => x.Id == OrderId);
        if (order is null)
        {
            throw new InvalidOperationException("Sipariş Bulunamadı");
        }
        if (order.MovieId != default)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.Id == Model.MovieId);
            if (movie is null)
            {
                throw new InvalidOperationException("Film Bulunamadı");
            }
            order.Price = movie.Price;
            order.MovieId = Model.MovieId;
        }
        _context.SaveChanges();
    }
}

public class UpdateOrderModel
{
    public int MovieId { get; set; }
}
