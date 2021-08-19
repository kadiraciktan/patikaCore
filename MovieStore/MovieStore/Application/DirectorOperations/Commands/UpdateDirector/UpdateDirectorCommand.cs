using AutoMapper;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        private readonly IMovieContext _context;
       
        public int DirectorId { get; set; }
        public UpdateDirectorModel Model { get; set; }

        public UpdateDirectorCommand(IMovieContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var director = _context.Directors.FirstOrDefault(x => x.Id == DirectorId);
            if(director is null)
            {
                throw new InvalidOperationException("Yönetmen Bulunamadı!");
            }
            var valid = _context.Directors.FirstOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname);
            if (valid is not null)
            {
                throw new InvalidOperationException("Aynı İsimde Bir Yönetmen Mevcut !");
            }

            director.Name = director.Name == default ? director.Name : Model.Name;
            director.Surname = director.Surname ==default ? director.Surname: Model.Surname;
            _context.SaveChanges();

        }

    }

    public class UpdateDirectorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
