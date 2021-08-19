using AutoMapper;
using MovieStore.DataAccess;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommand
    {
        private readonly IMovieContext _context;
        private readonly IMapper _mapper;
        public CreateActorModel Model { get; set; }
        public CreateActorCommand(IMovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var actor = _context.Actors.FirstOrDefault(x=>x.Name==Model.Name && x.Surname==Model.Surname);
            if (actor is not null)
            {
                throw new InvalidOperationException("Bu Aktör Zaten Mevcut.");
            }
            var result = _mapper.Map<Actor>(Model);
            _context.Actors.Add(result);
            _context.SaveChanges();
        }

    }
    public class CreateActorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
