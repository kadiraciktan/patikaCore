using MovieStore.DataAccess;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.TestSetup
{
    public static class Actors
    {
        public static void AddActors(this MovieContext context)
        {
            context.Actors.AddRange(
                     new ActorsDetailView() { Id = 1, Name = "Vin", Surname = "Diesel" },
                     new ActorsDetailView() { Id = 2, Name = "Dwayne", Surname = "Johnson" },
                     new ActorsDetailView() { Id = 3, Name = "Charlize", Surname = "Theron" },
                     new ActorsDetailView() { Id = 4, Name = "Jason", Surname = "Statham" },
                     new ActorsDetailView() { Id = 5, Name = "Keanu", Surname = "Reeves" },
                     new ActorsDetailView() { Id = 6, Name = "Chris", Surname = "Hemsworth" },
                     new ActorsDetailView() { Id = 7, Name = "Scarlett", Surname = "Johanson" },
                     new ActorsDetailView() { Id = 8, Name = "Morgan", Surname = "Freeman" },
                     new ActorsDetailView() { Id = 9, Name = "Cate", Surname = "Blanchett" },
                     new ActorsDetailView() { Id = 10, Name = "Liv", Surname = "Tyler" },
                     new ActorsDetailView() { Id = 11, Name = "Chloe Grace", Surname = "Moretz" },
                     new ActorsDetailView() { Id = 12, Name = "Tom", Surname = "Holland" }
           );
        }
    }
}
