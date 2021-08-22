using FluentAssertions;
using MovieStore.Application.ActorOperations.Commands.DeleteActor;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.ActorOperations.Commands.DeleteCommand
{
    public class DeleteActorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieContext _context;
        public DeleteActorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-0)]
        [InlineData(null)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = id;
            DeleteActorCommandValidator validationRules= new DeleteActorCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = 2;
            DeleteActorCommandValidator validationRules = new DeleteActorCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}
