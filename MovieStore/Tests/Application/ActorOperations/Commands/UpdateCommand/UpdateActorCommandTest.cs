using AutoMapper;
using FluentAssertions;
using MovieStore.Application.ActorOperations.Commands.UpdateActor;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.ActorOperations.Commands.UpdateCommand
{
    public class UpdateActorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieContext _context;
        private readonly IMapper _mapper;

        public UpdateActorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData("a","b",-1)]
        [InlineData("a", "", -1)]
        [InlineData("a", "b", 1)]
        [InlineData("a", "", 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShoulBeReturnErrors(string name,string surname,int actorId)
        {
            UpdateActorCommand command = new UpdateActorCommand(_context);
            command.ActorId= actorId;
            command.Model = new UpdateActorModel()
            {
                Name=name,
                Surname=surname
            };

            UpdateActorCommandValidator validationRules = new UpdateActorCommandValidator();
            var result = validationRules.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {

            UpdateActorCommand command = new UpdateActorCommand(_context);
            command.ActorId = 1;
            command.Model = new UpdateActorModel()
            {
                Name = "Test",
                Surname = "Test2"
            };

            UpdateActorCommandValidator validationRules = new UpdateActorCommandValidator();
            var result = validationRules.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldNotBeReturnErrors()
        {

            UpdateActorCommand command = new UpdateActorCommand(_context);
            command.ActorId = 1;
            command.Model = new UpdateActorModel()
            {
                Name = "Test",
                Surname = "Test2"
            };
            FluentActions.Invoking(() => command.Handle()).Should().NotThrow();
        }


        [Fact]
        public void WhenInValidInputsAreGiven_Actor_ShouldBeReturnErrors()
        {

            UpdateActorCommand command = new UpdateActorCommand(_context);
            command.ActorId = -1;
            command.Model = new UpdateActorModel()
            {
                Name = "Test",
                Surname = "Test2"
            };
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }
    }
}
