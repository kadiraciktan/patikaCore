using AutoMapper;
using FluentAssertions;
using MovieStore.Application.MovieOperations.Commands.DeleteMovie;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.MovieOperations.Commands.DeleteCommand
{
    public class DeleteMovieCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public DeleteMovieCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(null)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
            DeleteMovieCommand command = new DeleteMovieCommand(_context,_mapper);
            command.MovieId = id;
            DeleteMovieCommandValidator validationRules = new DeleteMovieCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Director_InAMovie_ShouldBeReturnErrors()
        {
            DeleteMovieCommand command = new DeleteMovieCommand(_context,_mapper);
            command.MovieId = 1;
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            DeleteMovieCommand command = new DeleteMovieCommand(_context,_mapper);
            command.MovieId = 1;
            DeleteMovieCommandValidator validationRules = new DeleteMovieCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInValidInputsAreGiven_Director_ShouldBeReturnErrors()
        {
            DeleteMovieCommand command = new DeleteMovieCommand(_context,_mapper);
            command.MovieId = 1;
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }
    }
}
