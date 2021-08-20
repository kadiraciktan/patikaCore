
using FluentValidation;

namespace MovieStore.Application.CustomerOperations.Commands.UpdateCommand;
public class UpdateCustomerCommandValidator:AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(command => command.CustomerId).GreaterThan(0);
        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(2);
        RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(2);
        RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(6);
    }
}
