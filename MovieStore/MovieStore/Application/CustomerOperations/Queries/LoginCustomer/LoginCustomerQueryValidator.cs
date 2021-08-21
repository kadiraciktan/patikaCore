
using FluentValidation;

namespace MovieStore.Application.CustomerOperations.Queries.LoginCustomer;
public class LoginCustomerQueryValidator:AbstractValidator<LoginCustomerQuery>
{
    public LoginCustomerQueryValidator()
    {
        RuleFor(query => query.Model.Email).EmailAddress().NotEmpty();
        RuleFor(query => query.Model.Password).MinimumLength(6).NotEmpty();
    }
}
