using AutoMapper;
using FluentAssertions;
using MovieStore.Application.OrderOperations.Commands.DeleteOrder;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.OrderOperations.Commands.DeleteCommand
{
    public class DeleteOrderCommandTest:IClassFixture<CommonTestFixture>
    {

        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public DeleteOrderCommandTest(CommonTestFixture testFixture)
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
            DeleteOrderCommand command = new DeleteOrderCommand(_context);
            command.OrderId = id;
            DeleteOrderCommandValidator validationRules = new DeleteOrderCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Order_ShouldBeReturnErrors()
        {
            DeleteOrderCommand command = new DeleteOrderCommand(_context);
            command.OrderId = 999;
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            DeleteOrderCommand command = new DeleteOrderCommand(_context);
            command.OrderId = 1;
            DeleteOrderCommandValidator validationRules = new DeleteOrderCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInValidInputsAreGiven_Order_ShouldBeReturnErrors()
        {
            DeleteOrderCommand command = new DeleteOrderCommand(_context);
            command.OrderId = 99;
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }
    }
}
