using AutoMapper;
using FluentAssertions;
using MovieStore.Application.OrderOperations.Commands.CreateOrder;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.OrderOperations.Commands.CreateCommand
{
    public class CreateOrderCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public CreateOrderCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int customerId, int MovieId)
        {
            CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);
            command.Model = new CreateOrderModel { MovieId=MovieId, CustomerId = customerId };
            CreateOrderCommandValidator validationRules = new CreateOrderCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);
            command.Model = new CreateOrderModel { MovieId = 1, CustomerId = 1 };
            CreateOrderCommandValidator validationRules = new CreateOrderCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Order_ShouldBeCreated()
        {
            CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);
            command.Model = new CreateOrderModel { MovieId = 1, CustomerId = 1 };

            FluentActions.Invoking(() => command.Handle()).Should().NotThrow();
        }


        [Fact]
        public void WhenInvalidMovieIdAreGiven_Order_ShouldBeCreated()
        {
            CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);
            command.Model = new CreateOrderModel { MovieId =999, CustomerId = 1 };

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }


        [Fact]
        public void WhenInvalidCustomerIdAreGiven_Order_ShouldBeCreated()
        {
            CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);
            command.Model = new CreateOrderModel { MovieId = 1, CustomerId = 999 };

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }

    }
}
