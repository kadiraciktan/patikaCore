using AutoMapper;
using FluentAssertions;
using MovieStore.Application.ActorOperations.Commands.CreateActor;
using MovieStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.TestSetup;
using Xunit;

namespace Tests.Application.ActorOperations.Commands.CreateCommand
{
    public class CreateActorCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public CreateActorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }



        //invalid bir giriş verildiğinde_validator_hata döndürmeli (hata sayısı 0 dan büyük olmalı)
        [Theory]
        [InlineData("","")]
        [InlineData("a","")]
        [InlineData("", "a")]
        [InlineData("aa", "a")]
        [InlineData("a", "aa")]
        //[InlineData("aa", "aa")] Valid
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name,string surname)
        {
            CreateActorCommand command = new CreateActorCommand(_context, _mapper);
            command.Model = new CreateActorModel { Name = name, Surname = surname };
            CreateActorCommandValidator validationRules = new CreateActorCommandValidator();
            var result  = validationRules.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
            
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            CreateActorCommand command = new CreateActorCommand(_context, _mapper);
            command.Model = new CreateActorModel { Name = "Emma", Surname = "Watson" };
            CreateActorCommandValidator validationRules = new CreateActorCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeCreated()
        {
            CreateActorCommand command = new CreateActorCommand(_context, _mapper);
            command.Model = new CreateActorModel { Name = "Emma", Surname = "Watson2" };

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var actor = _context.Actors.FirstOrDefault(x => x.Name == command.Model.Name && x.Surname == command.Model.Surname);
            actor.Should().NotBeNull();
            actor.Name.Should().Be(command.Model.Name);
            actor.Surname.Should().Be(command.Model.Surname);
        }

    }
}
