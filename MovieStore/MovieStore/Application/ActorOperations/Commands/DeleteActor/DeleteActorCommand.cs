using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommand
    {
        public int ActorId {  get; set; }
        private readonly IMovieContext _context;

        public DeleteActorCommand(IMovieContext context)
        {
            _context = context;
        }


        public void Handle()
        {
            var actor = _context.Actors.FirstOrDefault(x=>x.Id==ActorId);
            if (actor is null)
            {
                throw new InvalidOperationException("Aktör Bulunamadı");
            }


            var movie = _context.MovieActors.FirstOrDefault(x => x.ActorId == ActorId);
            if (movie is not null)
            {
                throw new InvalidOperationException("Aktör Silinemez! Film Mevcut");

            }

            _context.Actors.Remove(actor);
            _context.SaveChanges();
        }

    }
}
