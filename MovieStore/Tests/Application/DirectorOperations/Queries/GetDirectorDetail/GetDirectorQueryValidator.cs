using AutoMapper;
using FluentAssertions;
using MovieStore.Application.DirectorOperations.Queries.GetDirectorDetail;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorQueryValidator : IClassFixture<CommonTestFixture>
    {

        private readonly IMovieContext _context;
        private readonly IMapper _mapper;
        public GetDirectorQueryValidator(CommonTestFixture testFixture)
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
            GetDirectorDetailQuery command = new GetDirectorDetailQuery(_context, _mapper);
            command.DirectorId = id;
            GetDirectorDetailQueryValidator validationRules = new GetDirectorDetailQueryValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            GetDirectorDetailQuery command = new GetDirectorDetailQuery(_context, _mapper);
            command.DirectorId = 1;
            GetDirectorDetailQueryValidator validationRules = new GetDirectorDetailQueryValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldNotBeReturnErrors()
        {
            GetDirectorDetailQuery command = new GetDirectorDetailQuery(_context, _mapper);
            command.DirectorId = 1;
            FluentActions.Invoking(() => command.Handle()).Should().NotThrow();
        }

    }
}
