using AutoMapper;
using FluentAssertions;
using MovieStore.Application.OrderOperations.Queries.GetOrderDetail;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.OrderOperations.Queries.GetOrderDetail
{
    public class GetOrderDetailQueryTest:IClassFixture<CommonTestFixture>
    {

        private readonly IMovieContext _context;
        private readonly IMapper _mapper;
        public GetOrderDetailQueryTest(CommonTestFixture testFixture)
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
            GetOrderDetailQuery command = new GetOrderDetailQuery(_context, _mapper);
            command.OrderId = id;
            GetOrderDetailQueryValidator validationRules = new GetOrderDetailQueryValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            GetOrderDetailQuery command = new GetOrderDetailQuery(_context, _mapper);
            command.OrderId = 1;
            GetOrderDetailQueryValidator validationRules = new GetOrderDetailQueryValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Order_ShouldNotBeReturnErrors()
        {
            GetOrderDetailQuery command = new GetOrderDetailQuery(_context, _mapper);
            command.OrderId = 1;
            FluentActions.Invoking(() => command.Handle()).Should().NotThrow();
        }
    }
}
