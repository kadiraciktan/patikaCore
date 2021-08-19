using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommandValidator:AbstractValidator<CreateDirectorComand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(2);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(2);

        }
    }
}
