using AutoMapper;
using MovieStore.DataAccess;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQuery
    {
        private readonly IMovieContext _context;
        private readonly IMapper _mapper;
        public int ActorId;
        public GetActorDetailQuery(IMovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ActorDetailViewModel Handle()
        {
            var actor = _context.Actors.FirstOrDefault(x => x.Id == ActorId);

            if (actor is null)
            {
                throw new InvalidOperationException("Aktör Bulunamadı");
            }

            return _mapper.Map<ActorDetailViewModel>(actor);

        }
    }

    public class ActorDetailViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
     
    }
}
