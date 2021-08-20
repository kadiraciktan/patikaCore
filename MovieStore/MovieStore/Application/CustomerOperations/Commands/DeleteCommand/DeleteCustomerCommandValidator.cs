
using FluentValidation;

namespace MovieStore.Application.CustomerOperations.Commands.DeleteCommand;
public class DeleteCustomerCommandValidator:AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(command=>command.CustomerId).GreaterThan(0);
    }
    
}
