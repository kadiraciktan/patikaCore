using AutoMapper;
using FluentAssertions;
using MovieStore.Application.CustomerOperations.Queries.GetCustomerDetail;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryTest: IClassFixture<CommonTestFixture>
    {

        private readonly IMovieContext _context;
        private readonly IMapper _mapper;
        public GetCustomerDetailQueryTest(CommonTestFixture testFixture)
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
            GetCustomerDetailQuery command = new GetCustomerDetailQuery(_context, _mapper);
            command.CustomerId = id;
            GetCustomerDetailQueryValidator validationRules = new GetCustomerDetailQueryValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            GetCustomerDetailQuery command = new GetCustomerDetailQuery(_context, _mapper);
            command.CustomerId = 1;
            GetCustomerDetailQueryValidator validationRules = new GetCustomerDetailQueryValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Customer_ShouldNotBeReturnErrors()
        {
            GetCustomerDetailQuery command = new GetCustomerDetailQuery(_context, _mapper);
            command.CustomerId = 1;
            FluentActions.Invoking(() => command.Handle()).Should().NotThrow();
        }

    }
}
