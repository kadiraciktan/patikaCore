using AutoMapper;
using MovieStore.DataAccess;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorComand
    {
        private readonly IMovieContext _context;
        private readonly IMapper _mapper;
        public CreateDirectorModel Model { get; set; }

        public CreateDirectorComand(IMovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var director = _context.Directors.FirstOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname);
            if(director is not null)
            {
                throw new InvalidOperationException("Bu Yönetmen Mevcut!");
            }
            var result = _mapper.Map<Director>(Model);
            _context.Directors.Add(result);
            _context.SaveChanges();

        }
        
    }

    public class CreateDirectorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
