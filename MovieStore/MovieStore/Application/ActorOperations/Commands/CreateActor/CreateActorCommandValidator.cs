using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommandValidator:AbstractValidator<CreateActorCommand>
    {
        public CreateActorCommandValidator()
        {
            RuleFor(command=>command.Model.Name).NotEmpty().MinimumLength(2);
            RuleFor(command=>command.Model.Surname).NotEmpty().MinimumLength(2);
        }
    }
}
