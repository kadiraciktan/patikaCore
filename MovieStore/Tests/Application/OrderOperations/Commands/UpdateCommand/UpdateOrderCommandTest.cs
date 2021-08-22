using AutoMapper;
using FluentAssertions;
using MovieStore.Application.OrderOperations.Commands.UpdateOrder;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.OrderOperations.Commands.UpdateCommand
{
   public class UpdateOrderCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieContext _context;
        private readonly IMapper _mapper;

        public UpdateOrderCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }


        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int orderId, int MovieId)
        {
            UpdateOrderCommand command = new UpdateOrderCommand(_context, _mapper);
            command.OrderId = orderId;
            command.Model = new UpdateOrderModel { MovieId = MovieId};
            UpdateOrderCommandValidator validationRules = new UpdateOrderCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {

            UpdateOrderCommand command = new UpdateOrderCommand(_context,_mapper);
      
            command.OrderId = 1;
            command.Model = new UpdateOrderModel()
            {
                MovieId = 1
            };

            UpdateOrderCommandValidator validationRules = new UpdateOrderCommandValidator();
            var result = validationRules.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldNotBeReturnErrors()
        {


            UpdateOrderCommand command = new UpdateOrderCommand(_context, _mapper);

            command.OrderId = 1;
            command.Model = new UpdateOrderModel()
            {
                MovieId = 1
            };
            FluentActions.Invoking(() => command.Handle()).Should().NotThrow();
        }


        [Fact]
        public void WhenInValidInputsAreGiven_Director_ShouldBeReturnErrors()
        {


            UpdateOrderCommand command = new UpdateOrderCommand(_context, _mapper);

            command.OrderId = 1;
            command.Model = new UpdateOrderModel()
            {
                MovieId = 1
            };
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }
    }
}

