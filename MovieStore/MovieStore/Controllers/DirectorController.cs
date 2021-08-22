using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Application.DirectorOperations.Commands.CreateDirector;
using MovieStore.Application.DirectorOperations.Commands.DeleteDirector;
using MovieStore.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStore.Application.DirectorOperations.Queries.GetDirectorDetail;
using MovieStore.Application.DirectorOperations.Queries.GetDirectors;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Controllers
{
    [Authorize]
    [Route("api/[controller]s")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IMovieContext _context;
        private readonly IMapper _mapper;

        public DirectorController(IMovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<DirectorsViewModel> GetDirectors()
        {
            GetDirectorsQuery query = new GetDirectorsQuery(_context, _mapper);
            return query.Handle();
        }

        [HttpGet("{id}")]
        public DirectorDetailModel GetDirectorDetail(int id)
        {
            GetDirectorDetailQuery query = new GetDirectorDetailQuery(_context, _mapper);
            query.DirectorId = id;
            GetDirectorDetailQueryValidator validationRules = new GetDirectorDetailQueryValidator();
            validationRules.ValidateAndThrow(query);
            return query.Handle();
        }

        [HttpPost]
        public IActionResult CreateDirector([FromBody] CreateDirectorModel model)
        {
            CreateDirectorComand command = new CreateDirectorComand(_context, _mapper);
            command.Model = model;
            CreateDirectorCommandValidator validationRules = new CreateDirectorCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();

            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult UpdateDirector([FromBody] UpdateDirectorModel model,int id)
        {
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            command.DirectorId = id;
            command.Model = model;
            UpdateDirectorCommandValidator validationRules = new UpdateDirectorCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDirector(int id)
        {
            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.DirectorId = id;
            DeleteDirectorCommandValidator validationRules = new DeleteDirectorCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

    }
}
