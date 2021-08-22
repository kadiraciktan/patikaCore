using AutoMapper;
using FluentAssertions;
using MovieStore.Application.DirectorOperations.Commands.CreateDirector;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.DirectorOperations.Commands.CreateCommand
{
    public class CreateDirectorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public CreateDirectorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }



        //invalid bir giriş verildiğinde_validator_hata döndürmeli (hata sayısı 0 dan büyük olmalı)
        [Theory]
        [InlineData("", "")]
        [InlineData("a", "")]
        [InlineData("", "a")]
        [InlineData("aa", "a")]
        [InlineData("a", "aa")]
        //[InlineData("aa", "aa")] Valid
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        {
            CreateDirectorComand command = new CreateDirectorComand(_context, _mapper);
            command.Model = new CreateDirectorModel { Name = name, Surname = surname };
            CreateDirectorCommandValidator validationRules = new CreateDirectorCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            CreateDirectorComand command = new CreateDirectorComand(_context, _mapper);
            command.Model = new CreateDirectorModel { Name = "Zack", Surname = "Synder" };
            CreateDirectorCommandValidator validationRules = new CreateDirectorCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Order_ShouldBeCreated()
        {
            CreateDirectorComand command = new CreateDirectorComand(_context, _mapper);
            command.Model = new CreateDirectorModel { Name = "Zack", Surname = "Synder" };

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var actor = _context.Directors.FirstOrDefault(x => x.Name == command.Model.Name && x.Surname == command.Model.Surname);
            actor.Should().NotBeNull();
            actor.Name.Should().Be(command.Model.Name);
            actor.Surname.Should().Be(command.Model.Surname);
        }

    }

}
