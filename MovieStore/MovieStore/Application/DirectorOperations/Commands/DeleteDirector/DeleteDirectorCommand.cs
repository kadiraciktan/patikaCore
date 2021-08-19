using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        private readonly IMovieContext _context;
        public int DirectorId { get; set; }

        public DeleteDirectorCommand(IMovieContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var director = _context.Directors.FirstOrDefault(x => x.Id == DirectorId);
            if (director is null)
            {
                throw new InvalidOperationException("Yönetmen Bulunamadı");
            }

            var movie = _context.Movies.FirstOrDefault(x=>x.DirectorId == director.Id);
            if (movie is not null)
            {
                throw new InvalidOperationException("Yönetmen Silinemez! Film Mevcut");
            }
            _context.Directors.Remove(director);
            _context.SaveChanges();

        } 
    }
}
