using FluentValidation;

namespace MovieStore.Application.DirectorOperations.Commands.UpdateDirector;
public class UpdateDirectorCommandValidator:AbstractValidator<UpdateDirectorCommand>
{
    public UpdateDirectorCommandValidator()
    {
        RuleFor(command => command.DirectorId).GreaterThan(0);
        RuleFor(command => command.Model.Name).MinimumLength(2);
        RuleFor(command => command.Model.Surname).MinimumLength(2);

    }
}
