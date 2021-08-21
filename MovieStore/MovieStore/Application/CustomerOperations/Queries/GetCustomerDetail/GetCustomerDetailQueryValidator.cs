
using FluentValidation;

namespace MovieStore.Application.CustomerOperations.Queries.GetCustomerDetail;
public class GetCustomerDetailQueryValidator:AbstractValidator<GetCustomerDetailQuery>
{
    public GetCustomerDetailQueryValidator()
    {
        RuleFor(query => query.CustomerId).NotEmpty().GreaterThan(0);
    }
}
