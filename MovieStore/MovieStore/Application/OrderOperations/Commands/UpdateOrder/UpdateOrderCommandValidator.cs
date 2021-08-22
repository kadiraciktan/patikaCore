
using FluentValidation;

namespace MovieStore.Application.OrderOperations.Commands.UpdateOrder;
public class UpdateOrderCommandValidator:AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Model.MovieId).NotEmpty().GreaterThan(0);
        RuleFor(x => x.OrderId).NotEmpty().GreaterThan(0);
    }
}
