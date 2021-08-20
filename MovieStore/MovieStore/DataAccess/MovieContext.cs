using Microsoft.EntityFrameworkCore;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.DataAccess
{
    public class MovieContext : DbContext, IMovieContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Customer>Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<MovieActor>().HasOne(x => x.Movie).WithMany(x => x.Actors).HasForeignKey(x => x.MovieId);

            modelBuilder.Entity<MovieActor>().HasKey(x => new { x.ActorId, x.MovieId });
            modelBuilder.Entity<MovieCustomer>().HasKey(x => new { x.MovieId, x.CustomerId, x.GenreId });


            modelBuilder.Entity<MovieActor>()
                .HasOne(x => x.Movie)
                .WithMany(x => x.MovieActors)
                .HasForeignKey(x => x.MovieId);


            modelBuilder.Entity<MovieActor>()
                .HasOne(x => x.Actor)
                .WithMany(x=>x.MovieActors)
                .HasForeignKey(x => x.ActorId);


            modelBuilder.Entity<MovieCustomer>()
                .HasOne(x=>x.Customer)
                .WithMany(x=>x.Movies)
                .HasForeignKey(x=> x.CustomerId);

            modelBuilder.Entity<MovieCustomer>()
                .HasOne(x=>x.Movie)
                .WithMany(x=>x.MovieCustomers)
                .HasForeignKey(x => x.MovieId);


        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
