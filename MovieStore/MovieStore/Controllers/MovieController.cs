using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Application.MovieOperations.Commands.CreateMovie;
using MovieStore.Application.MovieOperations.Commands.DeleteMovie;
using MovieStore.Application.MovieOperations.Commands.UpdateMovie;
using MovieStore.Application.MovieOperations.Queries.GetMovieDetail;
using MovieStore.Application.MovieOperations.Queries.GetMovies;
using MovieStore.DataAccess;

namespace MovieStore.Controllers;
[Authorize]
[Route("api/[controller]s")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;

    public MovieController(IMovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public List<MoviesViewModel> GetMovies()
    {
        GetMoviesQuery query = new GetMoviesQuery(_context, _mapper);
        return query.Handle();
    }

    [HttpGet("{id}")]
    public MovieDetailModel GetMovieDetail(int id)
    {
        GetMovieDetailQuery query = new GetMovieDetailQuery(_context, _mapper);
        query.MovieId = id;
        GetMovieDetailQueryValidator validationRules = new GetMovieDetailQueryValidator();
        validationRules.ValidateAndThrow(query);
        return query.Handle();
    }

    [HttpPost]
    public IActionResult CreateMovie([FromBody] CreateMovieModel model)
    {
        CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
        command.Model = model;
        CreateMovieCommandValidator validationRules = new CreateMovieCommandValidator();
        validationRules.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMovie(int id,[FromBody]UpdateMovieModel model)
    {
        UpdateMovieCommand command = new UpdateMovieCommand(_context,_mapper);
        command.Model = model;
        command.MovieId = id;
        UpdateMovieCommandValidator validationRules = new UpdateMovieCommandValidator();
        validationRules.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id)
    {
        DeleteMovieCommand command = new DeleteMovieCommand(_context,_mapper);
        command.MovieId = id;
        DeleteMovieCommandValidator validationRules = new DeleteMovieCommandValidator();
        validationRules.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }
}
