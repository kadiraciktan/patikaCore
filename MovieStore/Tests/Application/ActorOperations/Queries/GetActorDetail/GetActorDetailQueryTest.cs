using AutoMapper;
using FluentAssertions;
using MovieStore.Application.ActorOperations.Queries.GetActorDetail;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieContext _context;
        private readonly IMapper _mapper;
        public GetActorDetailQueryTest(CommonTestFixture testFixture)
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
            GetActorDetailQuery command = new GetActorDetailQuery(_context, _mapper);
            command.ActorId = id;
            GetActorDetailQueryValidator validationRules = new GetActorDetailQueryValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            GetActorDetailQuery command = new GetActorDetailQuery(_context, _mapper);
            command.ActorId = 2;
            GetActorDetailQueryValidator validationRules = new GetActorDetailQueryValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldNotBeReturnErrors()
        {
            GetActorDetailQuery command = new GetActorDetailQuery(_context, _mapper);
            command.ActorId = 2;
            FluentActions.Invoking(() => command.Handle()).Should().NotThrow();
        }


    }
}
