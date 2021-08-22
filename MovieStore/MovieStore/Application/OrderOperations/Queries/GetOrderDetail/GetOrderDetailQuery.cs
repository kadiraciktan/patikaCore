
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DataAccess;

namespace MovieStore.Application.OrderOperations.Queries.GetOrderDetail;
public class GetOrderDetailQuery
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;
    public int OrderId { get; set; }
    public GetOrderDetailQuery(IMovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public OrderDetailViewModel Handle()
    {
        var order = _context.Orders
            .Include(x => x.Customer)
            .Include(x => x.Movie)
            .FirstOrDefault(x=>x.Id==OrderId);

        var result = _mapper.Map<OrderDetailViewModel>(order);

        return result;
    }
}

public class OrderDetailViewModel
{
    public int Id { get; set; }
    public DateTime BoughtTime { get; set; }
    public float Price { get; set; }
    public OrderDetailCustomerModel Customer { get; set; }
    public OrderDetailMovieModel Movie { get; set; }
    public bool IsActive { get; set; }
}

public class OrderDetailCustomerModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class OrderDetailMovieModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}

