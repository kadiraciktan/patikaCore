
using FluentValidation;

namespace MovieStore.Application.CustomerOperations.Commands.CreateCommand;
public class CreateCustomerCommandValidator:AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(command => command.Model.Email).NotEmpty().EmailAddress();
        RuleFor(command=>command.Model.Name).NotEmpty().MinimumLength(2);
        RuleFor(command=>command.Model.Surname).NotEmpty().MinimumLength(2);
        RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(6);
    }
}
