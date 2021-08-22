﻿
using FluentValidation;

namespace MovieStore.Application.OrderOperations.Queries.GetOrderDetail;
public class GetOrderDetailQueryValidator:AbstractValidator<GetOrderDetailQuery>
{
    public GetOrderDetailQueryValidator()
    {
        RuleFor(query => query.OrderId).NotEmpty().GreaterThan(0);
    }
}
