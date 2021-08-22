using AutoMapper;
using FluentAssertions;
using MovieStore.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.DirectorOperations.Commands.UpdateCommand
{
    public class UpdateDirectorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieContext _context;
        private readonly IMapper _mapper;

        public UpdateDirectorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData("a", "b", -1)]
        [InlineData("a", "", -1)]
        [InlineData("a", "b", 1)]
        [InlineData("a", "", 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShoulBeReturnErrors(string name, string surname, int actorId)
        {
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            command.DirectorId = actorId;
            command.Model = new UpdateDirectorModel()
            {
                Name = name,
                Surname = surname
            };

            UpdateDirectorCommandValidator validationRules = new UpdateDirectorCommandValidator();
            var result = validationRules.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {

            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            command.DirectorId = 1;
            command.Model = new UpdateDirectorModel()
            {
                Name = "Test",
                Surname = "Test2"
            };

            UpdateDirectorCommandValidator validationRules = new UpdateDirectorCommandValidator();
            var result = validationRules.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Order_ShouldNotBeReturnErrors()
        {

            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            command.DirectorId = 1;
            command.Model = new UpdateDirectorModel()
            {
                Name = "Test",
                Surname = "Test2"
            };
            FluentActions.Invoking(() => command.Handle()).Should().NotThrow();
        }


        [Fact]
        public void WhenInValidInputsAreGiven_Order_ShouldBeReturnErrors()
        {

            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            command.DirectorId = -1;
            command.Model = new UpdateDirectorModel()
            {
                Name = "Test",
                Surname = "Test2"
            };
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }
    }
}
