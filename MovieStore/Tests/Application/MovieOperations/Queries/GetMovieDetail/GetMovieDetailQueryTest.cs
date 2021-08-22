using AutoMapper;
using FluentAssertions;
using MovieStore.Application.MovieOperations.Queries.GetMovieDetail;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryTest : IClassFixture<CommonTestFixture>
    {

        private readonly IMovieContext _context;
        private readonly IMapper _mapper;
        public GetMovieDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-0)]
        [InlineData(null)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
            GetMovieDetailQuery command = new GetMovieDetailQuery(_context, _mapper);
            command.MovieId = id;
            GetMovieDetailQueryValidator validationRules = new GetMovieDetailQueryValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            GetMovieDetailQuery command = new GetMovieDetailQuery(_context, _mapper);
            command.MovieId = 1;
            GetMovieDetailQueryValidator validationRules = new GetMovieDetailQueryValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldNotBeReturnErrors()
        {
            GetMovieDetailQuery command = new GetMovieDetailQuery(_context, _mapper);
            command.MovieId = 1;
            FluentActions.Invoking(() => command.Handle()).Should().NotThrow();
        }

    }
}
