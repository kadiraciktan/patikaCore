using AutoMapper;
using FluentAssertions;
using MovieStore.Application.MovieOperations.Commands.UpdateMovie;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.MovieOperations.Commands.UpdateCommand
{
    public class UpdateMovieCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieContext _context;
        private readonly IMapper _mapper;

        public UpdateMovieCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Theory]
        [InlineData(0,0,0,0,"a")]
        [InlineData(1, 0, 0, 0, "a")]
        [InlineData(0, 1, 0, 0, "a")]
        [InlineData(0, 0, 1, 0, "a")]
        [InlineData(0, 0, 0, 1, "a")]
        [InlineData(1, 1, 0, 0, "a")]
        [InlineData(1, 1, 1, 0, "a")]
        [InlineData(1, 1, 1, 0, "")]
        [InlineData(1, 1, 1, 1, "a")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int movieId, int genreId, int directorId, float price, string name)
        {
            UpdateMovieCommand command = new UpdateMovieCommand(_context, _mapper);
            command.MovieId= movieId;
            command.Model = new UpdateMovieModel()
            {
                DirectorId = directorId,
                GenreId = genreId,
                MovieActors = new List<int> { 1, 2, 3 },
                Name = name,
                Price = price,
                Year = DateTime.Now.Date.AddYears(-5)
            };
            UpdateMovieCommandValidator validationRules = new UpdateMovieCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            UpdateMovieCommand command = new UpdateMovieCommand(_context, _mapper);
            command.MovieId = 1;
            command.Model = new UpdateMovieModel()
            {
                DirectorId = 1,
                GenreId = 1,
                MovieActors = new List<int> { 1, 2, 3 },
                Name = "yeniiii",
                Price = 11f,
                Year = DateTime.Now.Date.AddYears(-5)
            };
            UpdateMovieCommandValidator validationRules = new UpdateMovieCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, "test")]
        [InlineData(999, 0, 0, 0, "test")]
        [InlineData(0, 999, 0, 0, "test")]
        [InlineData(0, 0, 999, 0, "test")]
        [InlineData(0, 0, 0, 999, "test")]
        [InlineData(999, 999, 0, 0, "test")]
        [InlineData(999, 999, 999, 0, "test")]
        [InlineData(999, 999, 999, 999, "test")]
        public void WhenInvalidInputsAreGiven_Movie_ShouldBeReturnErrors(int movieId, int genreId, int directorId, float price, string name)
        {
            UpdateMovieCommand command = new UpdateMovieCommand(_context, _mapper);
            command.MovieId = movieId;
            command.Model = new UpdateMovieModel()
            {
                DirectorId = directorId,
                GenreId = genreId,
                MovieActors = new List<int> { 1, 2, 3 },
                Name = name,
                Price = price,
                Year = DateTime.Now.Date.AddYears(-5)
            };
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void WhenValidInputsAreGiven_Movie_ShouldNotBeReturnErrors()
        {
            UpdateMovieCommand command = new UpdateMovieCommand(_context, _mapper);
            command.MovieId = 1;
            command.Model = new UpdateMovieModel()
            {
                DirectorId = 1,
                GenreId = 1,
                MovieActors = new List<int> { 1, 2, 3 },
                Name = "yeniiii",
                Price = 11f,
                Year = DateTime.Now.Date.AddYears(-5)
            };
            FluentActions.Invoking(() => command.Handle()).Should().NotThrow();

        }

    }
}
