
using AutoMapper;
using MovieStore.DataAccess;
using MovieStore.Entities;

namespace MovieStore.Application.OrderOperations.Commands.CreateOrder;
public class CreateOrderCommand
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;
    public CreateOrderModel Model { get; set; }

    public CreateOrderCommand(IMovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var customer = _context.Customers.FirstOrDefault(x => x.Id == Model.CustomerId);
        if (customer is null)
        {
            throw new InvalidOperationException("Müşteri Bulunamadı");
        }
        var movie = _context.Movies.FirstOrDefault(x => x.Id == Model.MovieId);
        if (movie is null)
        {
            throw new InvalidOperationException("Film Bulunamadı");
        }

        var result = _mapper.Map<Order>(Model);
        result.Price = movie.Price;

        _context.Orders.Add(result);
        _context.SaveChanges();
    }
}

public class CreateOrderModel
{
    public int CustomerId { get; set; }
    public int MovieId { get; set; }
    public DateTime BoughtTime { get; set; }

}
