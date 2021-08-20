using AutoMapper;
using MovieStore.Application.ActorOperations.Commands.CreateActor;
using MovieStore.Application.ActorOperations.Commands.UpdateActor;
using MovieStore.Application.ActorOperations.Queries.GetActorDetail;
using MovieStore.Application.ActorOperations.Queries.GetActors;
using MovieStore.Application.DirectorOperations.Commands.CreateDirector;
using MovieStore.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStore.Application.DirectorOperations.Queries.GetDirectorDetail;
using MovieStore.Application.DirectorOperations.Queries.GetDirectors;
using MovieStore.Application.GenreOperations.Queries.GetGenres;
using MovieStore.Application.MovieOperations.Commands.CreateMovie;
using MovieStore.Application.MovieOperations.Commands.UpdateMovie;
using MovieStore.Application.MovieOperations.Queries.GetMovieDetail;
using MovieStore.Application.MovieOperations.Queries.GetMovies;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Genre, GenresViewModel>();

            CreateMap<Actor, ActorsViewModel>();
            CreateMap<Actor, ActorDetailViewModel>();
            CreateMap<CreateActorModel, Actor>();
            CreateMap<UpdateActorModel, Actor>();

            CreateMap<Director, DirectorsViewModel>();
            CreateMap<Director, DirectorDetailModel>();
            CreateMap<CreateDirectorModel, Director>();
            CreateMap<UpdateDirectorModel, Director>();

            CreateMap<Movie, MoviesViewModel>();
            CreateMap<CreateMovieModel, Movie>();

            CreateMap<Movie, CreateMovieModel>().ForMember(x => x.MovieActors,
                opt => opt.MapFrom(x => x.Id));

            CreateMap<Movie, MoviesViewModel>().ForMember(
          dest => dest.Actors,
          opt => opt.MapFrom(src => src.MovieActors.Select(x => new GetMoviesActorsModel { Id = x.ActorId, Name = x.Actor.Name, Surname = x.Actor.Surname })));

            CreateMap<Movie, CreateMovieModel>().ForMember(
          dest => dest.MovieActors,
          opt => opt.MapFrom(src => src.MovieActors.Select(x => x.ActorId)));

            CreateMap<CreateMovieModel, Movie>().ForMember(
         dest => dest.MovieActors,
         opt => opt.MapFrom(src => src.MovieActors.Select(x => new MovieActor() { ActorId = x })));

            CreateMap<Movie, MovieDetailModel>().ForMember(
          dest => dest.Actors,
          opt => opt.MapFrom(src => src.MovieActors.Select(x => new GetMovieDetailActorsModel { Id = x.ActorId, Name = x.Actor.Name, Surname = x.Actor.Surname })));

            CreateMap<UpdateMovieModel, Movie>().ForMember(
          dest => dest.MovieActors,
          opt => opt.MapFrom(src => src.MovieActors.Select(x => new MovieActor() { ActorId = x })));
          
            CreateMap<Movie, UpdateMovieModel>().ForMember(
   dest => dest.MovieActors,
   opt => opt.MapFrom(src => src.MovieActors.Select(x => x.ActorId)));



        }
    }
}
