
using FluentValidation;

namespace MovieStore.Application.OrderOperations.Commands.DeleteOrder;
public class DeleteOrderCommandValidator:AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().GreaterThan(0);
    }
}

