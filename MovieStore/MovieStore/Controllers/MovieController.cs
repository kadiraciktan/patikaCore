using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Application.MovieOperations.Commands.CreateMovie;
using MovieStore.Application.MovieOperations.Queries.GetMovies;
using MovieStore.DataAccess;

namespace MovieStore.Controllers;
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

    [HttpPost]
    public IActionResult CreateMovie([FromBody] CreateMovieModel model)
    {
        CreateMovieCommand command = new CreateMovieCommand(_context,_mapper);
        command.Model = model;
        CreateMovieCommandValidator validationRules = new CreateMovieCommandValidator();
        validationRules.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }
}
