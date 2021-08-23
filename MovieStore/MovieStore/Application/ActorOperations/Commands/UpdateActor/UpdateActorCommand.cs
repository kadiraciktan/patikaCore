using AutoMapper;
using MovieStore.DataAccess;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommand
    {
        private readonly IMovieContext _context;
        public UpdateActorModel Model;
        public int ActorId {  get; set; }

        public UpdateActorCommand(IMovieContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var actor = _context.Actors.FirstOrDefault(x =>x.Id == ActorId);
            if (actor is null)
            {
                throw new InvalidOperationException("Aktör Bulunamadı");
            }
            actor.Name = Model.Name == default ? actor.Name : Model.Name;
            actor.Surname = Model.Surname == default ? actor.Surname : Model.Surname;

            var valid = _context.Actors.FirstOrDefault(x => x.Name == actor.Name && x.Surname == actor.Surname);
            if (valid is not null)
            {
                throw new InvalidOperationException("Aynı İsimde Bir Aktör Mevcut");
            }
            _context.SaveChanges();
        }
    }

    public class UpdateActorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }

}
