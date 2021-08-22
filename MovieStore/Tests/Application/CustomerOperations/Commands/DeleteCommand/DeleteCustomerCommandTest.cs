using AutoMapper;
using FluentAssertions;
using MovieStore.Application.CustomerOperations.Commands.DeleteCommand;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.CustomerOperations.Commands.DeleteCommand
{
    public class DeleteCustomerCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public DeleteCustomerCommandTest(CommonTestFixture testFixture)
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
            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
            command.CustomerId = id;
            DeleteCustomerCommandValidator validationRules = new DeleteCustomerCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Customer_ABoughtMovie_ShouldBeReturnErrors()
        {
            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
            command.CustomerId = 1;
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
            command.CustomerId = 1;
            DeleteCustomerCommandValidator validationRules = new DeleteCustomerCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInValidInputsAreGiven_Customer_ShouldBeReturnErrors()
        {
            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
            command.CustomerId = 1;
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }
    }
}
