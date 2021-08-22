using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Application.ActorOperations.Commands.CreateActor;
using MovieStore.Application.ActorOperations.Commands.DeleteActor;
using MovieStore.Application.ActorOperations.Commands.UpdateActor;
using MovieStore.Application.ActorOperations.Queries.GetActorDetail;
using MovieStore.Application.ActorOperations.Queries.GetActors;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Controllers
{
    //[Authorize]
    [Route("api/[controller]s")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IMovieContext _context;
        private readonly IMapper _mapper;
        
        public ActorController(IMovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<ActorsViewModel> GetActors()
        {
            GetActorsQuery query = new GetActorsQuery(_context, _mapper);
            return query.Handle();
        }

        [HttpGet("{id}")]
        public ActorDetailViewModel GetActorDetail(int id)
        {
            GetActorDetailQuery query = new GetActorDetailQuery(_context, _mapper); 
            query.ActorId = id;
            GetActorDetailQueryValidator validationRules = new GetActorDetailQueryValidator();
            validationRules.ValidateAndThrow(query);
            return query.Handle();
        }

        [HttpPost]
        public IActionResult CreateActor([FromBody] CreateActorModel model)
        {
            CreateActorCommand command = new CreateActorCommand(_context, _mapper);
            command.Model = model;
            CreateActorCommandValidator validationRules = new CreateActorCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateActor([FromBody] UpdateActorModel model, int id)
        {
            UpdateActorCommand command = new UpdateActorCommand(_context);
            command.ActorId = id;
            command.Model = model;
            UpdateActorCommandValidator validationRules = new UpdateActorCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteActor(int id)
        {
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = id;
            DeleteActorCommandValidator validationRules = new DeleteActorCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

    }
}
