using AutoMapper;
using MovieStore.Application.ActorOperations.Commands.CreateActor;
using MovieStore.Application.ActorOperations.Commands.UpdateActor;
using MovieStore.Application.ActorOperations.Queries.GetActorDetail;
using MovieStore.Application.ActorOperations.Queries.GetActors;
using MovieStore.Application.CustomerOperations.Commands;
using MovieStore.Application.CustomerOperations.Commands.CreateCommand;
using MovieStore.Application.DirectorOperations.Commands.CreateDirector;
using MovieStore.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStore.Application.DirectorOperations.Queries.GetDirectorDetail;
using MovieStore.Application.DirectorOperations.Queries.GetDirectors;
using MovieStore.Application.GenreOperations.Queries.GetGenres;
using MovieStore.Application.MovieOperations.Commands.CreateMovie;
using MovieStore.Application.MovieOperations.Commands.UpdateMovie;
using MovieStore.Application.MovieOperations.Queries.GetMovieDetail;
using MovieStore.Application.MovieOperations.Queries.GetMovies;
using MovieStore.Application.OrderOperations.Commands;
using MovieStore.Application.OrderOperations.Commands.CreateOrder;
using MovieStore.Application.OrderOperations.Queries.GetOrderDetail;
using MovieStore.Application.OrderOperations.Queries.GetOrders;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MovieStore.Application.CustomerOperations.Queries.GetCustomerDetail.GetCustomerDetailQuery;
using static MovieStore.Application.CustomerOperations.Queries.GetCustomers.GetCustomersQuery;

namespace MovieStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Genre
            CreateMap<Genre, GenresViewModel>();
            #endregion

            #region Actor
            CreateMap<Actor, ActorsViewModel>();
            CreateMap<Actor, ActorDetailViewModel>();
            CreateMap<CreateActorModel, Actor>();
            CreateMap<UpdateActorModel, Actor>();
            #endregion

            #region Director
            CreateMap<Director, DirectorsViewModel>();
            CreateMap<Director, DirectorDetailModel>();
            CreateMap<CreateDirectorModel, Director>();
            CreateMap<UpdateDirectorModel, Director>();
            #endregion

            #region Movie
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


            #endregion

            #region Customer
            CreateMap<CreateCustomerModel, Customer>();

            CreateMap<Customer, CustomersViewModel>()
                .ForMember(dest => dest.BougthMovies,
                opt => opt.MapFrom(src => src.BougthMovies.Select(x => new CustormersMovieModel { Id = x.Id, Name = x.Name })))
                .ForMember(dest => dest.FavoriteGenres,
                opt => opt.MapFrom(src => src.FavoriteGenres.Select(x => new CustormersGenreModel { Id = x.Id, Name = x.Name })));


            CreateMap<Customer, CustomerDetailViewModel>()
              .ForMember(dest => dest.BougthMovies,
              opt => opt.MapFrom(src => src.BougthMovies.Select(x => new CustomerDetailMovieModel { Id = x.Id, Name = x.Name })))
              .ForMember(dest => dest.FavoriteGenres,
              opt => opt.MapFrom(src => src.FavoriteGenres.Select(x => new CustomerDetailGenreModel { Id = x.Id, Name = x.Name })));


            #endregion

            #region Order
            CreateMap<CreateOrderModel, Order>();
              


            CreateMap<CreateMovieModel, Movie>().ForMember(
         dest => dest.MovieActors,
         opt => opt.MapFrom(src => src.MovieActors.Select(x => new MovieActor() { ActorId = x })));


            CreateMap<Order, OrdersViewModel>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => new OrdersCustomerModels { Id = src.CustomerId, Name = src.Customer.Name }))
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => new OrdersMovieModel { Id = src.MovieId, Name = src.Movie.Name }));


            CreateMap<Order, OrderDetailViewModel>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => new OrderDetailCustomerModel { Id = src.CustomerId, Name = src.Customer.Name }))
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => new OrderDetailMovieModel { Id = src.MovieId, Name = src.Movie.Name }));

            #endregion


            //CreateMap<kaynak,hedef>()
            //.ForMember(dest=> // hedefin neresine müdahale edicez , opt=> // nasıl müdahale edicez
        }
    }
}
