using AutoMapper;
using FluentAssertions;
using MovieStore.Application.CustomerOperations.Commands.UpdateCommand;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.CustomerOperations.Commands.UpdateCommand
{
    public class UpdateCustomerCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Theory]
        [InlineData("a", "b", "123", -1)]
        [InlineData("a", "", "123456", -1)]
        [InlineData("a", "b", "12346", 1)]
        [InlineData("a", "", "", 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShoulBeReturnErrors(string name, string surname, string password, int customerId)
        {
            UpdateCustomerCommand command = new UpdateCustomerCommand(_context, _mapper);
            command.CustomerId = customerId;
            command.Model = new UpdateCustomerModel()
            {
                Name = name,
                Surname = surname,
                Password = password
            };

            UpdateCustomerCommandValidator validationRules = new UpdateCustomerCommandValidator();
            var result = validationRules.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {

            UpdateCustomerCommand command = new UpdateCustomerCommand(_context, _mapper);
            command.CustomerId = 1;
            command.Model = new UpdateCustomerModel()
            {
                Name = "Test",
                Surname = "Test2",
                Password = "2345678899"
            };

            UpdateCustomerCommandValidator validationRules = new UpdateCustomerCommandValidator();
            var result = validationRules.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Customer_ShouldNotBeReturnErrors()
        {

            UpdateCustomerCommand command = new UpdateCustomerCommand(_context, _mapper);
            command.CustomerId = 1;
            command.Model = new UpdateCustomerModel()
            {
                Name = "Test",
                Surname = "Test2",
                   Password = "2345678899"
            };
            FluentActions.Invoking(() => command.Handle()).Should().NotThrow();
        }


        [Fact]
        public void WhenInValidInputsAreGiven_Customer_ShouldBeReturnErrors()
        {

            UpdateCustomerCommand command = new UpdateCustomerCommand(_context,_mapper);
            command.CustomerId = -1;
            command.Model = new UpdateCustomerModel()
            {
                Name = "Test",
                Surname = "Test2"
            };
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }

    }
}
