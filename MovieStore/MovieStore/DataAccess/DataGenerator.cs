using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieStore.Application.ActorOperations.Queries.GetActors;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.DataAccess
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieContext(serviceProvider.GetRequiredService<DbContextOptions<MovieContext>>()))
            {
                List<Actor> actors = new List<Actor>() {
                  new Actor() { Id = 1, Name = "Vin", Surname = "Diesel" },
                        new Actor() { Id = 2, Name = "Dwayne", Surname = "Johnson" },
                        new Actor() { Id = 3, Name = "Charlize", Surname = "Theron" },
                        new Actor() { Id = 4, Name = "Jason", Surname = "Statham" },
                        new Actor() { Id = 5, Name = "Keanu", Surname = "Reeves" },
                        new Actor() { Id = 6, Name = "Chris", Surname = "Hemsworth" },
                        new Actor() { Id = 7, Name = "Scarlett", Surname = "Johanson" },
                        new Actor() { Id = 8, Name = "Morgan", Surname = "Freeman" },
                        new Actor() { Id = 9, Name = "Cate", Surname = "Blanchett" },
                        new Actor() { Id = 10, Name = "Liv", Surname = "Tyler" },
                        new Actor() { Id = 11, Name = "Chloe Grace", Surname = "Moretz" },
                        new Actor() { Id = 12, Name = "Tom", Surname = "Holland" }
                };
                List<Genre> genres = new List<Genre>() {
                 new Genre() { Id = 1, Name = "COMEDY" },
                        new Genre() { Id = 2, Name = "SCI-FI" },
                        new Genre() { Id = 3, Name = "HORROR" },
                        new Genre() { Id = 4, Name = "ROMANCE" },
                        new Genre() { Id = 5, Name = "ACTION" },
                        new Genre() { Id = 6, Name = "THRILLER" },
                        new Genre() { Id = 7, Name = "DRAMA" },
                        new Genre() { Id = 8, Name = "MYSTERY" },
                        new Genre() { Id = 9, Name = "CRIME" },
                        new Genre() { Id = 10, Name = "ADVENTURE" },
                        new Genre() { Id = 11, Name = "FANTASY" },
                        new Genre() { Id = 12, Name = "SUPERHERO" }
                };

                List<Director> directors = new List<Director>() {
                    new Director { Id = 1, Name = "Justin", Surname = "Lin" }
                };

                List<Movie> movies = new List<Movie>()
                {
                    new Movie()
                    {
                        Id=1,
                        Name ="Fast And Furious",
                        Director=directors[0],
                        Price=10f,
                        GenreId=5,
                        Year=DateTime.Now.Date.AddYears(-3),
                        MovieActors = new List<MovieActor>(){
                            new MovieActor(){ActorId=1},
                            new MovieActor(){ActorId=4},
                            new MovieActor(){ActorId=3},
                        }
                    }
                };

                List<Customer> customers = new List<Customer>()
                {
                    new Customer(){ 
                        Id = 1,
                        Email="test@test.com",
                        Name="Kadir",
                        Surname="AÇIKTAN",
                        Password="123456",
                        BougthMovies= new List<Movie>(){ movies[0]},
                        FavoriteGenres= new List<Genre>{ genres[4]}
                    }
                };

                if (!context.Genres.Any())
                {
                    context.Genres.AddRange(genres);
                }


                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(actors);
                }

                if (!context.Directors.Any())
                {
                    context.Directors.AddRange(directors);
                }

                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(movies);
                }

                if (!context.Customers.Any())
                {
                    context.Customers.AddRange(customers);
                }

                context.SaveChanges();

            }
        }
    }
}
