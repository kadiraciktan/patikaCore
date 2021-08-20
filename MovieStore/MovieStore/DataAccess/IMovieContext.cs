﻿using Microsoft.EntityFrameworkCore;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.DataAccess
{
    public interface IMovieContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet <Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }

        public DbSet<MovieActor> MovieActors {  get; set; }
        public DbSet<Director> Directors {  get; set; }
        int SaveChanges();
    }
}