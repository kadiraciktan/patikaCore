using AutoMapper;
using FluentAssertions;
using MovieStore.Application.CustomerOperations.Commands.CreateCommand;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.CustomerOperations.Commands.CreateCommand
{
    public class CreateCustomerCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public CreateCustomerCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Theory]
        [InlineData("asd","123","asdaasd","asdasdasdas")]
        [InlineData("test@test.com", "123", "asdaasd", "asdasdasdas")]
        [InlineData("test@test.com", "123456", "", "")]
        [InlineData("test@test.com", "123456", "asasdds", "")]
        [InlineData("test@test.com", "123456", "", "asdaasdsd")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string email, string password,string name, string surname)
        {
            CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
            command.Model = new CreateCustomerModel { Email=email,Name=name,Password=password,Surname=surname };
            CreateCustomerCommandValidator validationRules = new CreateCustomerCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
            command.Model = new CreateCustomerModel { Email = "demo@test.com", Name = "deneme", Password = "123456789", Surname = "testi" };
            CreateCustomerCommandValidator validationRules = new CreateCustomerCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().Be(0);

        }

        [Theory]       
        [InlineData("test@test.com", "123456123", "aaaaaaa", "bbbbbbbb")]
        public void WhenInvalidInputsAreGiven_Customer_ShouldBeReturnErrors(string email, string password, string name, string surname)
        {
            CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
            command.Model = new CreateCustomerModel { Email = email, Name = name, Password = password, Surname = surname };
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void WhenValidInputsAreGiven_Customer_ShouldNotBeReturnErrors()
        {
            CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
            command.Model = new CreateCustomerModel { Email = "demo@test.com", Name = "deneme", Password = "123456789", Surname = "testi" };
            FluentActions.Invoking(() => command.Handle()).Should().NotThrow();
        }


    }
}
