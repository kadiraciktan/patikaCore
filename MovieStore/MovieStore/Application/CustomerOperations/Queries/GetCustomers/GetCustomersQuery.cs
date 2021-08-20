
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DataAccess;

namespace MovieStore.Application.CustomerOperations.Queries.GetCustomers;
public class GetCustomersQuery
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;

    public GetCustomersQuery(IMovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public List<CustomersViewModel> Handle()
    {
        var list = _context.Customers
            .Include(x=>x.BougthMovies)
            .Include(x=>x.FavoriteGenres)
            .ToList();

        var result = _mapper.Map<List<CustomersViewModel>>(list);
        return result;

    }

    public class CustomersViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<CustormersGenreModel> FavoriteGenres { get; set; }
        public List<CustormersMovieModel> BougthMovies { get; set; }
    }

    public class CustormersMovieModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CustormersGenreModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
