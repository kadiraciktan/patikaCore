using AutoMapper;
using FluentAssertions;
using MovieStore.Application.MovieOperations.Commands.CreateMovie;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.MovieOperations.Commands.CreateCommand
{
    public class CreateMovieCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieContext _context;
        private readonly IMapper _mapper;
        public CreateMovieCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData("a", 0, 0, 0)]
        [InlineData("ab", 0, 0, 0)]
        [InlineData("a", 1, 0, 0)]
        [InlineData("ab", 1, 0, 0)]
        [InlineData("a", 1, 1, 0)]
        [InlineData("ab", 1, 1, 0)]
        [InlineData("a", 1, 1, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShoulBeReturnErrors(string name, int genreId, int directorId, float price)
        {
            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            command.Model = new CreateMovieModel()
            {
                Name = name,
                GenreId = genreId,
                DirectorId = directorId,
                Price = price
            };
            CreateMovieCommandValidator validationRules = new CreateMovieCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShoulNotBeReturnErrors()
        {
            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            command.Model = new CreateMovieModel()
            {
                Name = "Test Film",
                GenreId = 1,
                DirectorId = 1,
                Price = 11f
            };
            CreateMovieCommandValidator validationRules = new CreateMovieCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().Be(0);
        }


        [Theory]
        [InlineData("testtesta", 999, 0, 0)]
        [InlineData("testtestab", 999, 999, 0)]
        [InlineData("atesttest", 999, 999, 999)]
        [InlineData("abtesttest", 999, 0, 0)]
        [InlineData("atesttest", 999, 999, 0)]
        [InlineData("testtest", 1, 999, 999)]
        public void WhenInvalidInputsAreGiven_Movie_ShoulBeReturnErrors(string name, int genreId, int directorId, float price)
        {
            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            command.Model = new CreateMovieModel()
            {
                Name = name,
                GenreId = genreId,
                DirectorId = directorId,
                Price = price,
                Year = DateTime.Now.Date.AddYears(-10),
                MovieActors = new List<int> { 1, 2, 3 }
            };
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();

        }

        [Fact]
        public void WhenValidInputsAreGiven_Movie_ShoulNotBeReturnErrors()
        {
            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            command.Model = new CreateMovieModel()
            {
                Name = "FilminAdı",
                GenreId = 1,
                DirectorId = 1,
                Price = 5,
                Year = DateTime.Now.Date.AddYears(-10),
                MovieActors = new List<int> { 1, 2, 3 }
            };
            FluentActions.Invoking(() => command.Handle()).Should().NotThrow();

        }




    }
}
