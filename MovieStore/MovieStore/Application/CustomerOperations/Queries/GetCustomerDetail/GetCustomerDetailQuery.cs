
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DataAccess;

namespace MovieStore.Application.CustomerOperations.Queries.GetCustomerDetail;
public class GetCustomerDetailQuery
{
    public int CustomerId { get; set; }
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;

    public GetCustomerDetailQuery(IMovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public CustomerDetailViewModel Handle()
    {
        var customer = _context.Customers
            .Include(x => x.BougthMovies)
            .Include(x => x.FavoriteGenres)
            .FirstOrDefault(x => x.Id == CustomerId);

        if (customer is null)
        {
            throw new InvalidOperationException("Müşteri Bulunamadı");
        }

        var result = _mapper.Map<CustomerDetailViewModel>(customer);

        return result;


    }

    public class CustomerDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<CustomerDetailGenreModel> FavoriteGenres { get; set; }
        public List<CustomerDetailMovieModel> BougthMovies { get; set; }
    }

    public class CustomerDetailMovieModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CustomerDetailGenreModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
