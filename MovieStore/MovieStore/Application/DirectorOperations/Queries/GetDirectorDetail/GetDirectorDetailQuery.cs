using AutoMapper;
using MovieStore.DataAccess;

namespace MovieStore.Application.DirectorOperations.Queries.GetDirectorDetail;
public class GetDirectorDetailQuery
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;
    public int DirectorId { get; set; }
    public GetDirectorDetailQuery(IMovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public DirectorDetailModel Handle()
    {
        var director = _context.Directors.FirstOrDefault(x => x.Id == DirectorId);
        if (director is null)
        {
            throw new InvalidOperationException("Yönetmen Bulunamadı!");
        }

        var result = _mapper.Map<DirectorDetailModel>(director);

        return result;

    }
}

public class DirectorDetailModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}
