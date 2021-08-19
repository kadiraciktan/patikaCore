using AutoMapper;
using MovieStore.DataAccess;

namespace MovieStore.Application.DirectorOperations.Queries.GetDirectors;

public class GetDirectorsQuery
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;

    public GetDirectorsQuery(IMovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<DirectorsViewModel> Handle()
    {
        var list = _context.Directors.ToList();
        var result = _mapper.Map<List<DirectorsViewModel>>(list);
        return result;
    }


}

public class DirectorsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}
