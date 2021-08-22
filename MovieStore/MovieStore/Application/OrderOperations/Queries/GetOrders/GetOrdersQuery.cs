
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DataAccess;

namespace MovieStore.Application.OrderOperations.Queries.GetOrders;
public class GetOrdersQuery
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;

    public GetOrdersQuery(IMovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<OrdersViewModel> Handle()
    {
        var orders = _context.Orders
            .Include(x=>x.Customer)
            .Include(x=>x.Movie)
            .Where(x=>x.IsActive)
            .ToList();

        var result = _mapper.Map<List<OrdersViewModel>>(orders);

        return result;
    }

}

public class OrdersViewModel
{
    public int Id { get; set; }
    public DateTime BoughtTime { get; set; }
    public float Price { get; set; }
    public OrdersCustomerModels Customer { get; set; }
    public OrdersMovieModel Movie { get; set; }
    public bool IsActive { get; set; }
}

public class OrdersCustomerModels
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class OrdersMovieModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}
